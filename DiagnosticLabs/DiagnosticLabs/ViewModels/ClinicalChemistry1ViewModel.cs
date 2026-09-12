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
    public class ClinicalChemistry1ViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "ClinicalChemistry1";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public ClinicalChemistry1 ClinicalChemistry1 { get; set; }
        public bool IsSetDefaultMode { get; set; } = false;
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }
        
        public ObservableCollection<string> Tests { get; set; }
        #endregion

        public ClinicalChemistry1ViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.ClinicalChemistry1);
            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewClinicalChemistry1();
            else
                LoadClinicalChemistry1(id);

            this.NewCommand = new RelayCommand(param => NewClinicalChemistry1());
            this.SaveCommand = new RelayCommand(param => SaveClinicalChemistry1());
            this.DeleteCommand = new RelayCommand(param => DeleteClinicalChemistry1());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SaveClinicalChemistry1Defaults());
        }

        #region Data Actions
        private void LoadClinicalChemistry1(long clinicalChemistry1Id)
        {
            this.ClinicalChemistry1 = _labResultsBLL.Get<ClinicalChemistry1>(clinicalChemistry1Id);

            if (this.ClinicalChemistry1.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.ClinicalChemistry1.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.ClinicalChemistry1.PatientId ?? 0,
                PatientCode = this.ClinicalChemistry1.PatientCode,
                PatientName = this.ClinicalChemistry1.PatientName,
                Age = this.ClinicalChemistry1.Age,
                Gender = this.ClinicalChemistry1.Sex
            };
        }

        private void NewClinicalChemistry1()
        {
            IsSetDefaultMode = false;
            string defaults = _commonFunctions.GetDefaults(_entityName);
            this.ClinicalChemistry1 = _labResultsBLL.NewRecord<ClinicalChemistry1>(this.ModuleId, defaults);
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
            this.ClinicalChemistry1 = _labResultsBLL.NewRecord<ClinicalChemistry1>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;
            this.ClearNotificationMessages();
        }

        private void SaveClinicalChemistry1()
        {
            base.SavePatientRegistration();

            if (!this.ClinicalChemistry1.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.ClinicalChemistry1.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.ClinicalChemistry1.Id;
            if (_labResultsBLL.SaveLabResult(this.ClinicalChemistry1, this.PatientRegistration, this.Patient, ref id))
            {
                this.ClinicalChemistry1.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SaveClinicalChemistry1Defaults()
        {
            this.ClinicalChemistry1.PatientId = this.Patient.Id;
            this.ClinicalChemistry1.PatientRegistrationId = this.PatientRegistration.Id;
            this.ClinicalChemistry1.PatientCode = this.Patient.PatientCode;
            this.ClinicalChemistry1.PatientName = this.Patient.PatientName;
            this.ClinicalChemistry1.Age = this.Patient.Age;
            this.ClinicalChemistry1.Sex = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.ClinicalChemistry1));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewClinicalChemistry1();
        }

        private void DeleteClinicalChemistry1()
        {
            if (this.ClinicalChemistry1.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.ClinicalChemistry1.Id;
            this.ClinicalChemistry1.IsActive = false;
            if (_labResultsBLL.Save(this.ClinicalChemistry1, ref id))
                this.NotificationMessage = Messages.DeletedSuccessfully;
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            ClinicalChemistry1 clinicalChemistry1 = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<ClinicalChemistry1>(patientRegistrationId) : null;

            if (clinicalChemistry1 != null)
                this.ClinicalChemistry1 = clinicalChemistry1;
            else
            {
                this.ClinicalChemistry1.PatientRegistrationId = patientRegistrationId;
                this.ClinicalChemistry1.PatientCode = this.Patient.PatientCode;
                this.ClinicalChemistry1.PatientName = this.Patient.PatientName;
                this.ClinicalChemistry1.Sex = this.Patient.Gender;
                this.ClinicalChemistry1.Age = this.Patient.Age;
                this.ClinicalChemistry1.CompanyOrPhysician = this.Patient.CompanyName;
            }
        }
        #endregion

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.ClinicalChemistry1Test);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.MedicalTechnologist);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Pathologist);
        }

        public override void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.ClinicalChemistry1Test:
                    this.Tests = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.ClinicalChemistry1Test, this.ModuleId, true));
                    if (this.ClinicalChemistry1 != null && this.ClinicalChemistry1.Test != string.Empty)
                        this.ClinicalChemistry1.Test = this.Tests.First();
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
