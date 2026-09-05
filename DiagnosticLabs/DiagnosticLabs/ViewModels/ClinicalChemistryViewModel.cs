using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class ClinicalChemistryViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "ClinicalChemistry";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public ClinicalChemistry ClinicalChemistry { get; set; }

        public bool IsSetDefaultMode { get; set; } = false;

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }
        #endregion

        public ClinicalChemistryViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.ClinicalChemistry);

            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewClinicalChemistry();
            else
                LoadClinicalChemistry(id);

            this.NewCommand = new RelayCommand(param => NewClinicalChemistry());
            this.SaveCommand = new RelayCommand(param => SaveClinicalChemistry());
            this.DeleteCommand = new RelayCommand(param => DeleteClinicalChemistry());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SaveClinicalChemistryDefaults());
        }

        #region Data Actions
        private void LoadClinicalChemistry(long clinicalChemistryId)
        {
            this.ClinicalChemistry = _labResultsBLL.Get<ClinicalChemistry>(clinicalChemistryId);

            if (this.ClinicalChemistry.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.ClinicalChemistry.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.ClinicalChemistry.PatientId ?? 0,
                PatientCode = this.ClinicalChemistry.PatientCode,
                PatientName = this.ClinicalChemistry.PatientName,
                Age = this.ClinicalChemistry.Age,
                Gender = this.ClinicalChemistry.Sex
            };
        }

        private void NewClinicalChemistry()
        {
            IsSetDefaultMode = false;

            string defaults = _commonFunctions.GetDefaults(_entityName);

            this.ClinicalChemistry = _labResultsBLL.NewRecord<ClinicalChemistry>(this.ModuleId, defaults);
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

            this.ClinicalChemistry = _labResultsBLL.NewRecord<ClinicalChemistry>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;

            this.ClearNotificationMessages();
        }

        private void SaveClinicalChemistry()
        {
            base.SavePatientRegistration();

            if (!this.ClinicalChemistry.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.ClinicalChemistry.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.ClinicalChemistry.Id;
            if (_labResultsBLL.SaveLabResult(this.ClinicalChemistry, this.PatientRegistration, this.Patient, ref id))
            {
                this.ClinicalChemistry.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SaveClinicalChemistryDefaults()
        {
            this.ClinicalChemistry.PatientId = this.Patient.Id;
            this.ClinicalChemistry.PatientRegistrationId = this.PatientRegistration.Id;
            this.ClinicalChemistry.PatientCode = this.Patient.PatientCode;
            this.ClinicalChemistry.PatientName = this.Patient.PatientName;
            this.ClinicalChemistry.Age = this.Patient.Age;
            this.ClinicalChemistry.Sex = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.ClinicalChemistry));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewClinicalChemistry();
        }

        private void DeleteClinicalChemistry()
        {
            if (this.ClinicalChemistry.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.ClinicalChemistry.Id;
            this.ClinicalChemistry.IsActive = false;
            if (_labResultsBLL.Save(this.ClinicalChemistry, ref id))
            {
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            ClinicalChemistry clinicalChemistry = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<ClinicalChemistry>(patientRegistrationId) : null;

            if (clinicalChemistry != null)
            {
                this.ClinicalChemistry = clinicalChemistry;
            }
            else
            {
                this.ClinicalChemistry.PatientRegistrationId = patientRegistrationId;
                this.ClinicalChemistry.PatientCode = this.Patient.PatientCode;
                this.ClinicalChemistry.PatientName = this.Patient.PatientName;
                this.ClinicalChemistry.Sex = this.Patient.Gender;
                this.ClinicalChemistry.Age = this.Patient.Age;
                this.ClinicalChemistry.CompanyOrPhysician = this.Patient.CompanyName;
            }
        }
        #endregion

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.MedicalTechnologist);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Pathologist);
        }

        public override void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
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
