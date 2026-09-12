using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SerologyViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "Serology";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public Serology Serology { get; set; }
        public bool IsSetDefaultMode { get; set; } = false;
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }
        public ObservableCollection<string> Tests { get; set; }
        #endregion

        public SerologyViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.Serology);
            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewSerology();
            else
                LoadSerology(id);

            this.NewCommand = new RelayCommand(param => NewSerology());
            this.SaveCommand = new RelayCommand(param => SaveSerology());
            this.DeleteCommand = new RelayCommand(param => DeleteSerology());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SaveSerologyDefaults());
        }

        #region Data Actions
        private void LoadSerology(long serologyId)
        {
            this.Serology = _labResultsBLL.Get<Serology>(serologyId);

            if (this.Serology.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.Serology.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.Serology.PatientId ?? 0,
                PatientCode = this.Serology.PatientCode,
                PatientName = this.Serology.PatientName,
                Age = this.Serology.Age,
                Gender = this.Serology.Sex
            };
        }

        private void NewSerology()
        {
            IsSetDefaultMode = false;
            string defaults = _commonFunctions.GetDefaults(_entityName);
            this.Serology = _labResultsBLL.NewRecord<Serology>(this.ModuleId, defaults);
            this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);
            this.Patient = _patientsBLL.NewPatient();
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;
            this.ClearNotificationMessages();
        }

        private void SetDefaults()
        {
            IsSetDefaultMode = true;
            string defaults = _commonFunctions.GetDefaults(_entityName);
            this.Serology = _labResultsBLL.NewRecord<Serology>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;
            this.ClearNotificationMessages();
        }

        private void SaveSerology()
        {
            base.SavePatientRegistration();

            if (!this.Serology.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.Serology.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Serology.Id;
            if (_labResultsBLL.SaveLabResult(this.Serology, this.PatientRegistration, this.Patient, ref id))
            {
                this.Serology.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SaveSerologyDefaults()
        {
            this.Serology.PatientId = this.Patient.Id;
            this.Serology.PatientRegistrationId = this.PatientRegistration.Id;
            this.Serology.PatientCode = this.Patient.PatientCode;
            this.Serology.PatientName = this.Patient.PatientName;
            this.Serology.Age = this.Patient.Age;
            this.Serology.Sex = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.Serology));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewSerology();
        }

        private void DeleteSerology()
        {
            if (this.Serology.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Serology.Id;
            this.Serology.IsActive = false;
            if (_labResultsBLL.Save(this.Serology, ref id))
                this.NotificationMessage = Messages.DeletedSuccessfully;
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            Serology serology = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<Serology>(patientRegistrationId) : null;

            if (serology != null)
                this.Serology = serology;
            else
            {
                this.Serology.PatientRegistrationId = patientRegistrationId;
                this.Serology.PatientCode = this.Patient.PatientCode;
                this.Serology.PatientName = this.Patient.PatientName;
                this.Serology.Sex = this.Patient.Gender;
                this.Serology.Age = this.Patient.Age;
                this.Serology.CompanyOrPhysician = this.Patient.CompanyName;
            }
        }
        #endregion

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.SerologyTest);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.MedicalTechnologist);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Pathologist);
        }

        public override void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.SerologyTest:
                    this.Tests = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.SerologyTest, this.ModuleId, true));
                    if (this.Serology != null && this.Serology.Test != string.Empty)
                        this.Serology.Test = this.Tests.First();
                    break;
                case SingleLineEntries.MedicalTechnologist:
                    base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.MedicalTechnologist);
                    break;
                case SingleLineEntries.Pathologist:
                    base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Pathologist);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
