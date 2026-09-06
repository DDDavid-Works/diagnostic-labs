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
    public class HematologyViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "Hematology";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public Hematology Hematology { get; set; }

        public bool IsSetDefaultMode { get; set; } = false;

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }
        #endregion

        public HematologyViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.Hematology);

            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewHematology();
            else
                LoadHematology(id);

            this.NewCommand = new RelayCommand(param => NewHematology());
            this.SaveCommand = new RelayCommand(param => SaveHematology());
            this.DeleteCommand = new RelayCommand(param => DeleteHematology());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SaveHematologyDefaults());
        }

        #region Data Actions
        private void LoadHematology(long hematologyId)
        {
            this.Hematology = _labResultsBLL.Get<Hematology>(hematologyId);

            if (this.Hematology.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.Hematology.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.Hematology.PatientId ?? 0,
                PatientCode = this.Hematology.PatientCode,
                PatientName = this.Hematology.PatientName,
                Age = this.Hematology.Age,
                Gender = this.Hematology.Sex
            };
        }

        private void NewHematology()
        {
            IsSetDefaultMode = false;

            string defaults = _commonFunctions.GetDefaults(_entityName);

            this.Hematology = _labResultsBLL.NewRecord<Hematology>(this.ModuleId, defaults);
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

            this.Hematology = _labResultsBLL.NewRecord<Hematology>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;

            this.ClearNotificationMessages();
        }

        private void SaveHematology()
        {
            base.SavePatientRegistration();

            if (!this.Hematology.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.Hematology.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Hematology.Id;
            if (_labResultsBLL.SaveLabResult(this.Hematology, this.PatientRegistration, this.Patient, ref id))
            {
                this.Hematology.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SaveHematologyDefaults()
        {
            this.Hematology.PatientId = this.Patient.Id;
            this.Hematology.PatientRegistrationId = this.PatientRegistration.Id;
            this.Hematology.PatientCode = this.Patient.PatientCode;
            this.Hematology.PatientName = this.Patient.PatientName;
            this.Hematology.Age = this.Patient.Age;
            this.Hematology.Sex = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.Hematology));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewHematology();
        }

        private void DeleteHematology()
        {
            if (this.Hematology.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Hematology.Id;
            this.Hematology.IsActive = false;
            if (_labResultsBLL.Save(this.Hematology, ref id))
            {
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            Hematology hematology = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<Hematology>(patientRegistrationId) : null;

            if (hematology != null)
            {
                this.Hematology = hematology;
            }
            else
            {
                this.Hematology.PatientRegistrationId = patientRegistrationId;
                this.Hematology.PatientCode = this.Patient.PatientCode;
                this.Hematology.PatientName = this.Patient.PatientName;
                this.Hematology.Sex = this.Patient.Gender;
                this.Hematology.Age = this.Patient.Age;
                this.Hematology.CompanyOrPhysician = this.Patient.CompanyName;
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