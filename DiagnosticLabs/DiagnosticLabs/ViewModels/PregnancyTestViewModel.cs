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
    public class PregnancyTestViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "PregnancyTest";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public PregnancyTest PregnancyTest { get; set; }
        public bool IsSetDefaultMode { get; set; } = false;
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }
        #endregion

        public PregnancyTestViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.PregnancyTest);
            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewPregnancyTest();
            else
                LoadPregnancyTest(id);

            this.NewCommand = new RelayCommand(param => NewPregnancyTest());
            this.SaveCommand = new RelayCommand(param => SavePregnancyTest());
            this.DeleteCommand = new RelayCommand(param => DeletePregnancyTest());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SavePregnancyTestDefaults());
        }

        #region Data Actions
        private void LoadPregnancyTest(long pregnancyTestId)
        {
            this.PregnancyTest = _labResultsBLL.Get<PregnancyTest>(pregnancyTestId);

            if (this.PregnancyTest.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.PregnancyTest.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.PregnancyTest.PatientId ?? 0,
                PatientCode = this.PregnancyTest.PatientCode,
                PatientName = this.PregnancyTest.PatientName,
                Age = this.PregnancyTest.Age,
                Gender = this.PregnancyTest.Sex
            };
        }

        private void NewPregnancyTest()
        {
            IsSetDefaultMode = false;
            string defaults = _commonFunctions.GetDefaults(_entityName);
            this.PregnancyTest = _labResultsBLL.NewRecord<PregnancyTest>(this.ModuleId, defaults);
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
            this.PregnancyTest = _labResultsBLL.NewRecord<PregnancyTest>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;
            this.ClearNotificationMessages();
        }

        private void SavePregnancyTest()
        {
            base.SavePatientRegistration();

            if (!this.PregnancyTest.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.PregnancyTest.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.PregnancyTest.Id;
            if (_labResultsBLL.SaveLabResult(this.PregnancyTest, this.PatientRegistration, this.Patient, ref id))
            {
                this.PregnancyTest.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SavePregnancyTestDefaults()
        {
            this.PregnancyTest.PatientId = this.Patient.Id;
            this.PregnancyTest.PatientRegistrationId = this.PatientRegistration.Id;
            this.PregnancyTest.PatientCode = this.Patient.PatientCode;
            this.PregnancyTest.PatientName = this.Patient.PatientName;
            this.PregnancyTest.Age = this.Patient.Age;
            this.PregnancyTest.Sex = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.PregnancyTest));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewPregnancyTest();
        }

        private void DeletePregnancyTest()
        {
            if (this.PregnancyTest.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.PregnancyTest.Id;
            this.PregnancyTest.IsActive = false;
            if (_labResultsBLL.Save(this.PregnancyTest, ref id))
                this.NotificationMessage = Messages.DeletedSuccessfully;
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            PregnancyTest pregnancyTest = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<PregnancyTest>(patientRegistrationId) : null;

            if (pregnancyTest != null)
                this.PregnancyTest = pregnancyTest;
            else
            {
                this.PregnancyTest.PatientRegistrationId = patientRegistrationId;
                this.PregnancyTest.PatientCode = this.Patient.PatientCode;
                this.PregnancyTest.PatientName = this.Patient.PatientName;
                this.PregnancyTest.Sex = this.Patient.Gender;
                this.PregnancyTest.Age = this.Patient.Age;
                this.PregnancyTest.CompanyOrPhysician = this.Patient.CompanyName;
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
