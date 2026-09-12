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
    public class ClinicalChemistry2ViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "ClinicalChemistry2";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public ClinicalChemistry2 ClinicalChemistry2 { get; set; }
        public bool IsSetDefaultMode { get; set; } = false;
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }
        #endregion

        public ClinicalChemistry2ViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.ClinicalChemistry2);
            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewClinicalChemistry2();
            else
                LoadClinicalChemistry2(id);

            this.NewCommand = new RelayCommand(param => NewClinicalChemistry2());
            this.SaveCommand = new RelayCommand(param => SaveClinicalChemistry2());
            this.DeleteCommand = new RelayCommand(param => DeleteClinicalChemistry2());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SaveClinicalChemistry2Defaults());
        }

        #region Data Actions
        private void LoadClinicalChemistry2(long clinicalChemistry2Id)
        {
            this.ClinicalChemistry2 = _labResultsBLL.Get<ClinicalChemistry2>(clinicalChemistry2Id);

            if (this.ClinicalChemistry2.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.ClinicalChemistry2.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.ClinicalChemistry2.PatientId ?? 0,
                PatientCode = this.ClinicalChemistry2.PatientCode,
                PatientName = this.ClinicalChemistry2.PatientName,
                Age = this.ClinicalChemistry2.Age,
                Gender = this.ClinicalChemistry2.Sex
            };
        }

        private void NewClinicalChemistry2()
        {
            IsSetDefaultMode = false;
            string defaults = _commonFunctions.GetDefaults(_entityName);
            this.ClinicalChemistry2 = _labResultsBLL.NewRecord<ClinicalChemistry2>(this.ModuleId, defaults);
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
            this.ClinicalChemistry2 = _labResultsBLL.NewRecord<ClinicalChemistry2>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;
            this.ClearNotificationMessages();
        }

        private void SaveClinicalChemistry2()
        {
            base.SavePatientRegistration();

            if (!this.ClinicalChemistry2.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.ClinicalChemistry2.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.ClinicalChemistry2.Id;
            if (_labResultsBLL.SaveLabResult(this.ClinicalChemistry2, this.PatientRegistration, this.Patient, ref id))
            {
                this.ClinicalChemistry2.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SaveClinicalChemistry2Defaults()
        {
            this.ClinicalChemistry2.PatientId = this.Patient.Id;
            this.ClinicalChemistry2.PatientRegistrationId = this.PatientRegistration.Id;
            this.ClinicalChemistry2.PatientCode = this.Patient.PatientCode;
            this.ClinicalChemistry2.PatientName = this.Patient.PatientName;
            this.ClinicalChemistry2.Age = this.Patient.Age;
            this.ClinicalChemistry2.Sex = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.ClinicalChemistry2));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewClinicalChemistry2();
        }

        private void DeleteClinicalChemistry2()
        {
            if (this.ClinicalChemistry2.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.ClinicalChemistry2.Id;
            this.ClinicalChemistry2.IsActive = false;
            if (_labResultsBLL.Save(this.ClinicalChemistry2, ref id))
                this.NotificationMessage = Messages.DeletedSuccessfully;
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            ClinicalChemistry2 clinicalChemistry2 = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<ClinicalChemistry2>(patientRegistrationId) : null;

            if (clinicalChemistry2 != null)
                this.ClinicalChemistry2 = clinicalChemistry2;
            else
            {
                this.ClinicalChemistry2.PatientRegistrationId = patientRegistrationId;
                this.ClinicalChemistry2.PatientCode = this.Patient.PatientCode;
                this.ClinicalChemistry2.PatientName = this.Patient.PatientName;
                this.ClinicalChemistry2.Sex = this.Patient.Gender;
                this.ClinicalChemistry2.Age = this.Patient.Age;
                this.ClinicalChemistry2.CompanyOrPhysician = this.Patient.CompanyName;
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
