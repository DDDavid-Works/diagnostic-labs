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
    public class ImmunologyViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "Immunology";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public Immunology Immunology { get; set; }
        public bool IsSetDefaultMode { get; set; } = false;
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }
        public ObservableCollection<string> Tests { get; set; }
        #endregion

        public ImmunologyViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.Immunology);
            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewImmunology();
            else
                LoadImmunology(id);

            this.NewCommand = new RelayCommand(param => NewImmunology());
            this.SaveCommand = new RelayCommand(param => SaveImmunology());
            this.DeleteCommand = new RelayCommand(param => DeleteImmunology());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SaveImmunologyDefaults());
        }

        #region Data Actions
        private void LoadImmunology(long immunologyId)
        {
            this.Immunology = _labResultsBLL.Get<Immunology>(immunologyId);

            if (this.Immunology.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.Immunology.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.Immunology.PatientId ?? 0,
                PatientCode = this.Immunology.PatientCode,
                PatientName = this.Immunology.PatientName,
                Age = this.Immunology.Age,
                Gender = this.Immunology.Sex
            };
        }

        private void NewImmunology()
        {
            IsSetDefaultMode = false;
            string defaults = _commonFunctions.GetDefaults(_entityName);
            this.Immunology = _labResultsBLL.NewRecord<Immunology>(this.ModuleId, defaults);
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
            this.Immunology = _labResultsBLL.NewRecord<Immunology>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;
            this.ClearNotificationMessages();
        }

        private void SaveImmunology()
        {
            base.SavePatientRegistration();

            if (!this.Immunology.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.Immunology.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Immunology.Id;
            if (_labResultsBLL.SaveLabResult(this.Immunology, this.PatientRegistration, this.Patient, ref id))
            {
                this.Immunology.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SaveImmunologyDefaults()
        {
            this.Immunology.PatientId = this.Patient.Id;
            this.Immunology.PatientRegistrationId = this.PatientRegistration.Id;
            this.Immunology.PatientCode = this.Patient.PatientCode;
            this.Immunology.PatientName = this.Patient.PatientName;
            this.Immunology.Age = this.Patient.Age;
            this.Immunology.Sex = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.Immunology));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewImmunology();
        }

        private void DeleteImmunology()
        {
            if (this.Immunology.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Immunology.Id;
            this.Immunology.IsActive = false;
            if (_labResultsBLL.Save(this.Immunology, ref id))
                this.NotificationMessage = Messages.DeletedSuccessfully;
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            Immunology immunology = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<Immunology>(patientRegistrationId) : null;

            if (immunology != null)
                this.Immunology = immunology;
            else
            {
                this.Immunology.PatientRegistrationId = patientRegistrationId;
                this.Immunology.PatientCode = this.Patient.PatientCode;
                this.Immunology.PatientName = this.Patient.PatientName;
                this.Immunology.Sex = this.Patient.Gender;
                this.Immunology.Age = this.Patient.Age;
                this.Immunology.CompanyOrPhysician = this.Patient.CompanyName;
            }
        }
        #endregion

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.ImmunologyTest);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.MedicalTechnologist);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Pathologist);
        }

        public override void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.ImmunologyTest:
                    this.Tests = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.ImmunologyTest, this.ModuleId, true));
                    if (this.Immunology != null && this.Immunology.Test != string.Empty)
                        this.Immunology.Test = this.Tests.First();
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
