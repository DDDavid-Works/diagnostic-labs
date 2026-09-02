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
    public class MERViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "MedicalExamination";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public MER MER { get; set; }

        public bool IsSetDefaultMode { get; set; } = false;

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }
        public ICommand SetDefaultsCommand { get; set; }
        public ICommand SaveDefaultsCommand { get; set; }

        public ObservableCollection<string> AssessmentDoneBys { get; set; }
        public ObservableCollection<string> PhysicianNames { get; set; }
        public ObservableCollection<string> PhysicianLicenses { get; set; }
        #endregion

        #region Fields
        public string AssessmentDoneByField { get { return SingleLineEntries.AssessmentDoneBy; } }
        public string PhysicianNameField { get { return SingleLineEntries.PhysicianName; } }
        public string PhysicianLicenseField { get { return SingleLineEntries.PhysicianLicense; } }
        #endregion

        public MERViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.MedicalExamination);

            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewMedicalExamination();
            else
                LoadMedicalExamination(id);

            this.NewCommand = new RelayCommand(param => NewMedicalExamination());
            this.SaveCommand = new RelayCommand(param => SaveMedicalExamination());
            this.DeleteCommand = new RelayCommand(param => DeleteMedicalExamination());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
            this.SetDefaultsCommand = new RelayCommand(param => SetDefaults());
            this.SaveDefaultsCommand = new RelayCommand(param => SaveMedicalExaminationDefaults());
        }

        #region Data Actions
        private void LoadMedicalExamination(long medicalExaminationId)
        {
            IsSetDefaultMode = false;

            this.MER = _labResultsBLL.Get<MER>(medicalExaminationId);
            SetBooleans(this.MER);

            if (this.MER.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.MER.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.MER.PatientId ?? 0,
                PatientName = this.MER.PatientName,
                ContactNumbers = this.MER.ContactNo,
                Age = this.MER.Age,
                Gender = this.MER.Gender,
                CivilStatus = this.MER.CivilStatus,
                CompanyName = this.MER.CompanyName
            };
        }

        private void NewMedicalExamination()
        {
            IsSetDefaultMode = false;

            string defaults = _commonFunctions.GetDefaults(_entityName);

            this.MER = _labResultsBLL.NewRecord<MER>(this.ModuleId, defaults);
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

            this.MER = _labResultsBLL.NewRecord<MER>(this.ModuleId, defaults, true);
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(1);
            this.Patient = _patientsBLL.GetPatient(1);
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;

            this.ClearNotificationMessages();
        }

        private void SaveMedicalExamination()
        {
            base.SavePatientRegistration();

            if (!this.MER.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.MER.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.MER.Id;

            this.MER.ChestXray = MEStringValue(this.MER.ChestXRayNValue, this.MER.ChestXRayFValue);
            this.MER.CBC = MEStringValue(this.MER.CBCNValue, this.MER.CBCFValue);
            this.MER.Urinalysis = MEStringValue(this.MER.UrinalysisNValue, this.MER.UrinalysisFValue);
            this.MER.Fecalysis = MEStringValue(this.MER.FecalysisNValue, this.MER.FecalysisFValue);
            this.MER.HBsAg = MEStringValue(this.MER.HBsAgNValue, this.MER.HBsAgFValue);
            this.MER.DrugTest2Panel = MEStringValue(this.MER.DrugTest2PanelNValue, this.MER.DrugTest2PanelFValue);
            this.MER.DrugTest4Panel = MEStringValue(this.MER.DrugTest4PanelNValue, this.MER.DrugTest4PanelFValue);

            if (_labResultsBLL.SaveLabResult(this.MER, this.PatientRegistration, this.Patient, ref id))
            {
                this.MER.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void SaveMedicalExaminationDefaults()
        {
            this.MER.PatientId = this.Patient.Id;
            this.MER.PatientRegistrationId = this.PatientRegistration.Id;
            this.MER.PatientName = this.Patient.PatientName;
            this.MER.Age = this.Patient.Age;
            this.MER.Gender = this.Patient.Gender;

            _commonFunctions.SaveDefaults(_entityName, JsonConvert.SerializeObject(this.MER));
            this.NotificationMessage = Messages.SavedSuccessfully;
            this.NewMedicalExamination();
        }

        private void DeleteMedicalExamination()
        {
            if (this.MER.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.MER.Id;
            this.MER.IsActive = false;
            if (_labResultsBLL.Save(this.MER, ref id))
            {
                //this.Payment = _paymentsBLL.GetLatestPayment();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        public override void GetPatientRegistration(long patientRegistrationId)
        {
            base.GetPatientRegistration(patientRegistrationId);

            MER mer = patientRegistrationId != 0 ? _labResultsBLL.GetByPatientRegistrationId<MER>(patientRegistrationId) : null;

            if (mer != null)
            {
                SetBooleans(mer);
                this.MER = mer;
            }
            else
            {
                this.MER.PatientRegistrationId = patientRegistrationId;
                this.MER.PatientName = this.Patient.PatientName;
                this.MER.Age = this.Patient.Age;
                this.MER.Gender = this.Patient.Gender;
                this.MER.CivilStatus = this.Patient.CivilStatus;
                this.MER.CompanyName = this.Patient.CompanyName;
            }
        }

        public override void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.AssessmentDoneBy:
                    this.AssessmentDoneBys = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.AssessmentDoneBy, this.ModuleId, true));
                    if (this.MER != null && this.MER.AssessmentDoneBy != string.Empty)
                        this.MER.AssessmentDoneBy = this.AssessmentDoneBys.First();
                    break;
                case SingleLineEntries.PhysicianName:
                    this.PhysicianNames = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.PhysicianName, this.ModuleId, true));
                    if (this.MER != null && this.MER.PhysicianName != string.Empty)
                        this.MER.PhysicianName = this.PhysicianNames.First();
                    break;
                case SingleLineEntries.PhysicianLicense:
                    this.PhysicianLicenses = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.PhysicianLicense, this.ModuleId, true));
                    if (this.MER != null && this.MER.PhysicianLicense != string.Empty)
                        this.MER.PhysicianLicense = this.PhysicianLicenses.First();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Gender);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.CivilStatus);

            RefreshLabResultsSingleLineEntryList(SingleLineEntries.AssessmentDoneBy);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.PhysicianName);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.PhysicianLicense);
        }

        private void SetBooleans(MER mer)
        {
            mer.ChestXRayNValue = MEBooleanValue(mer.ChestXray, "N");
            mer.ChestXRayFValue = MEBooleanValue(mer.ChestXray, "F");
            mer.CBCNValue = MEBooleanValue(mer.CBC, "N");
            mer.CBCFValue = MEBooleanValue(mer.CBC, "F");
            mer.UrinalysisNValue = MEBooleanValue(mer.Urinalysis, "N");
            mer.UrinalysisFValue = MEBooleanValue(mer.Urinalysis, "F");
            mer.FecalysisNValue = MEBooleanValue(mer.Fecalysis, "N");
            mer.FecalysisFValue = MEBooleanValue(mer.Fecalysis, "F");
            mer.HBsAgNValue = MEBooleanValue(mer.HBsAg, "N");
            mer.HBsAgFValue = MEBooleanValue(mer.HBsAg, "F");
            mer.DrugTest2PanelNValue = MEBooleanValue(mer.DrugTest2Panel, "N");
            mer.DrugTest2PanelFValue = MEBooleanValue(mer.DrugTest2Panel, "F");
            mer.DrugTest4PanelNValue = MEBooleanValue(mer.DrugTest4Panel, "N");
            mer.DrugTest4PanelFValue = MEBooleanValue(mer.DrugTest4Panel, "F");
        }

        private string MEStringValue(bool? nValue, bool? fValue)
        {
            if (nValue == false && fValue == false)
                return null;
            else
            {
                if ((nValue != null && nValue == true) && (fValue == null || fValue == false))
                {
                    return "N";
                }
                else if ((fValue != null && fValue == true) && (nValue == null || nValue == false))
                {
                    return "F";
                }
                return null;
            }
        }

        private bool MEBooleanValue(string value, string reverseValue)
        {
            return value == null ? false : value.Trim() == reverseValue;
        }
        #endregion
    }
}