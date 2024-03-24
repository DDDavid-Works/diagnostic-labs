using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class APEViewModel : BaseLabResultsViewModel
    {
        private const string _entityName = "APE";

        CommonFunctions _commonFunctions = new CommonFunctions();
        LabResultsBLL _labResults = new LabResultsBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public APE APE { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshLabResultsSingleLineEntryListCommand { get; set; }

        public ObservableCollection<string> TextFields { get; set; }
        public ObservableCollection<string> ENTs { get; set; }
        public ObservableCollection<string> Gastroenterologies { get; set; }
        public ObservableCollection<string> Respiratories { get; set; }
        public ObservableCollection<string> IntegumentarySkins { get; set; }
        public ObservableCollection<string> Cardiologies { get; set; }
        public ObservableCollection<string> Psychologies { get; set; }
        public ObservableCollection<string> Endocrinologies { get; set; }
        public ObservableCollection<string> OBGyneUrologies { get; set; }
        public ObservableCollection<string> MuscoloSkeletals { get; set; }
        public ObservableCollection<string> InfectiousCommunicables { get; set; }
        public ObservableCollection<string> Neurologies { get; set; }
        public ObservableCollection<string> Surgicals { get; set; }
        public ObservableCollection<string> VitalSignsBys { get; set; }
        public ObservableCollection<string> HeightWeightBys { get; set; }
        #endregion

        #region Fields
        public string ENTField { get { return SingleLineEntries.APEENT; } }
        public string GastroenterologyField { get { return SingleLineEntries.APEGastroenterology; } }
        public string RespiratoryField { get { return SingleLineEntries.APERespiratory; } }
        public string IntegumentarySkinField { get { return SingleLineEntries.APEIntegumentarySkin; } }
        public string CardiologyField { get { return SingleLineEntries.APECardiology; } }
        public string PsychologyField { get { return SingleLineEntries.APEPsychology; } }
        public string EndocrinologyField { get { return SingleLineEntries.APEEndocrinology; } }
        public string OBGyneUrologyField { get { return SingleLineEntries.APEOBGyneUrology; } }
        public string MuscoloSkeletalField { get { return SingleLineEntries.APEMuscoloskeletal; } }
        public string InfectiousCommunicableField { get { return SingleLineEntries.APEInfectiousCommunicable; } }
        public string NeurologicalField { get { return SingleLineEntries.APENeurological; } }
        public string SurgicalField { get { return SingleLineEntries.APESurgical; } }
        public string OthersPastField { get { return MultiLineEntries.APEOthersPast; } }
        public string MedicationsField { get { return MultiLineEntries.APEMedications; } }
        public string AllergiesField { get { return MultiLineEntries.APEAllergies; } }
        public string ReviewOfSystemsField { get { return MultiLineEntries.APEReviewOfSystems; } }
        public string IsSmokingField { get { return ChoiceEntries.APEIsSmoking; } }
        public string SmokingSinceWhenField { get { return SingleLineEntries.APESmokingSinceWhen; } }
        public string NumberOfSticksPerDayField { get { return SingleLineEntries.APENumberOfSticksPerDay; } }
        public string DrinkingSinceWhenField { get { return SingleLineEntries.APEDrinkingSinceWhen; } }
        public string NumberOfBottlesField { get { return SingleLineEntries.APENumberOfBottles; } }
        public string LMPField { get { return SingleLineEntries.APELMP; } }
        public string BP1stField { get { return SingleLineEntries.APEBP1st; } }
        public string BP2ndField { get { return SingleLineEntries.APEBP2nd; } }
        public string CardiacRate1stField { get { return SingleLineEntries.APECardiacRate1st; } }
        public string CardiacRate2ndField { get { return SingleLineEntries.APECardiacRate2nd; } }
        public string HeightField { get { return SingleLineEntries.APEHeight; } }
        public string WeightField { get { return SingleLineEntries.APEWeight; } }
        public string BMIField { get { return MultiLineEntries.APEBMI; } }
        public string VARightEyeWGlassesField { get { return SingleLineEntries.APEVARightEyeWGlasses; } }
        public string VARightEyeWOGlassesField { get { return SingleLineEntries.APEVARightEyeWOGlasses; } }
        public string VALeftEyeWGlassesField { get { return SingleLineEntries.APEVALeftEyeWGlasses; } }
        public string VALeftEyeWOGlassesField { get { return SingleLineEntries.APEVALeftEyeWOGlasses; } }
        public string FindingsField { get { return MultiLineEntries.APEFindings; } }
        public string VitalSignsByField { get { return SingleLineEntries.VitalSignsBy; } }
        public string HeightWeightByField { get { return SingleLineEntries.HeightWeightBy; } }
        #endregion

        public APEViewModel(long id)
        {
            this.ModuleId = _commonFunctions.GetModuleId(Modules.AnnualPhysicalExam);

            if (id == 0)
                NewAPE();
            else
                LoadAPE(id);

            LoadAllSingleLineEntryLists();
            AddSetDefaultOptions();

            this.NewCommand = new RelayCommand(param => NewAPE());
            this.SaveCommand = new RelayCommand(param => SaveAPE());
            this.DeleteCommand = new RelayCommand(param => DeleteAPE());
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.RefreshLabResultsSingleLineEntryListCommand = new RelayCommand(param => RefreshLabResultsSingleLineEntryList((string)param));
        }

        #region Data Actions
        private void LoadAPE(long apeId)
        {
            this.APE = _labResults.Get<APE>(apeId);
            SetBooleans(this.APE);

            if (this.APE.PatientRegistrationId != 0)
                this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration((long)this.APE.PatientRegistrationId);
            else
                this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);

            this.Patient = new Patient()
            {
                Id = this.APE.PatientId ?? 0,
                PatientCode = string.Empty,
                PatientName = this.APE.PatientName,
                Age = this.APE.Age,
                Gender = this.APE.Gender
            };
        }

        private void NewAPE()
        {
            this.APE = _labResults.NewRecord<APE>(this.ModuleId);
            this.PatientRegistration = _patientRegistrationsBLL.NewPatientRegistration(false);
            this.Patient = _patientsBLL.NewPatient();
            this.SelectedCompany = null;
            this.SelectedBatchName = string.Empty;

            this.ClearNotificationMessages();
        }

        private void SaveAPE()
        {
            base.SavePatientRegistration();

            if (!this.APE.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.APE.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.APE.Id;

            if (this.APE.DrinkingDaily)
                this.APE.DrinkingFrequency = "Daily";
            else if (this.APE.DrinkingWeekly)
                this.APE.DrinkingFrequency = "Weekly";
            else if (this.APE.DrinkingOccasional)
                this.APE.DrinkingFrequency = "Occasional";

            if (this.APE.LMPTypeRegular)
                this.APE.LMPType = "Regular";
            else if (this.APE.LMPTypeIrregular)
                this.APE.LMPType = "Irregular";

            if (this.APE.VisualAcuityNormal)
                this.APE.VisualAcuity = "Normal";
            else if (this.APE.VisualAcuityEOR)
                this.APE.VisualAcuity = "EOR";
            else if (this.APE.VisualAcuityCorrected)
                this.APE.VisualAcuity = "Corrected";

            this.APE.Skin = PEStringValue(this.APE.SkinNValue, this.APE.SkinFValue);
            this.APE.HeadScalp = PEStringValue(this.APE.HeadScalpNValue, this.APE.HeadScalpFValue);
            this.APE.Eyes = PEStringValue(this.APE.EyesNValue, this.APE.EyesFValue);
            this.APE.Ears = PEStringValue(this.APE.EarsNValue, this.APE.EarsFValue);
            this.APE.Nose = PEStringValue(this.APE.NoseNValue, this.APE.NoseFValue);
            this.APE.TeethTonsilsThroatPharynx = PEStringValue(this.APE.TTTPNValue, this.APE.TTTPFValue);
            this.APE.NeckLymphNodesThyroid = PEStringValue(this.APE.NLNTNValue, this.APE.NLNTFValue);
            this.APE.ThoraxBreast = PEStringValue(this.APE.TBNValue, this.APE.TBFValue);
            this.APE.HeartLungs = PEStringValue(this.APE.HLNValue, this.APE.HLFValue);
            this.APE.AbdomenLiverSpleen = PEStringValue(this.APE.ALSNValue, this.APE.ALSFValue);
            this.APE.InguinalAreaGenitalsAnus = PEStringValue(this.APE.IAGANValue, this.APE.IAGAFValue);
            this.APE.ExtremetiesSpine = PEStringValue(this.APE.ExSNValue, this.APE.ExSFValue);
            this.APE.Tattoo = PEStringValue(this.APE.TattooNValue, this.APE.TattooFValue);
            this.APE.MassCyst = PEStringValue(this.APE.MassCystNValue, this.APE.MassCystFValue);
            this.APE.OthersPE = PEStringValue(this.APE.OthersPENValue, this.APE.OthersPEFValue);

            if (_labResults.SaveLabResultWithPatientRegistrationAndPatient(this.APE, this.PatientRegistration, this.Patient, ref id))
            {
                this.APE.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeleteAPE()
        {
            if (this.APE.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.APE.Id;
            this.APE.IsActive = false;
            if (_labResults.Save(this.APE, ref id))
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

            APE ape = _labResults.GetByPatientRegistrationId<APE>(patientRegistrationId);

            if (ape != null)
            {
                SetBooleans(ape);
                this.APE = ape;
            }
            else
            {
                this.APE.PatientRegistrationId = patientRegistrationId;
                this.APE.PatientName = this.Patient.PatientName;
                this.APE.Gender = this.Patient.Gender;
                this.APE.Age = this.Patient.Age;
            }
        }

        public override void RefreshLabResultsSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.APEENT:
                    this.ENTs = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APEENT, this.ModuleId, true));
                    if (this.APE != null && this.APE.ENT != string.Empty)
                        this.APE.ENT = this.ENTs.First();
                    break;
                case SingleLineEntries.APEGastroenterology:
                    this.Gastroenterologies = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APEGastroenterology, this.ModuleId, true));
                    if (this.APE != null && this.APE.Gastroenterology != string.Empty)
                        this.APE.Gastroenterology = this.Gastroenterologies.First();
                    break;
                case SingleLineEntries.APERespiratory:
                    this.Respiratories = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APERespiratory, this.ModuleId, true));
                    if (this.APE != null && this.APE.Respiratory != string.Empty)
                        this.APE.Respiratory = this.Respiratories.First();
                    break;
                case SingleLineEntries.APEIntegumentarySkin:
                    this.IntegumentarySkins = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APEIntegumentarySkin, this.ModuleId, true));
                    if (this.APE != null && this.APE.IntegumentarySkin != string.Empty)
                        this.APE.IntegumentarySkin = this.IntegumentarySkins.First();
                    break;
                case SingleLineEntries.APECardiology:
                    this.Cardiologies = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APECardiology, this.ModuleId, true));
                    if (this.APE != null && this.APE.Cardiology != string.Empty)
                        this.APE.Cardiology = this.Cardiologies.First();
                    break;
                case SingleLineEntries.APEPsychology:
                    this.Psychologies = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APEPsychology, this.ModuleId, true));
                    if (this.APE != null && this.APE.Psychology != string.Empty)
                        this.APE.Psychology = this.Psychologies.First();
                    break;
                case SingleLineEntries.APEEndocrinology:
                    this.Endocrinologies = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APEEndocrinology, this.ModuleId, true));
                    if (this.APE != null && this.APE.Endocrinology != string.Empty)
                        this.APE.Endocrinology = this.Endocrinologies.First();
                    break;
                case SingleLineEntries.APEOBGyneUrology:
                    this.OBGyneUrologies = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APEOBGyneUrology, this.ModuleId, true));
                    if (this.APE != null && this.APE.OBGyneUrology != string.Empty)
                        this.APE.OBGyneUrology = this.OBGyneUrologies.First();
                    break;
                case SingleLineEntries.APEMuscoloskeletal:
                    this.MuscoloSkeletals = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APEMuscoloskeletal, this.ModuleId, true));
                    if (this.APE != null && this.APE.Muscoloskeletal != string.Empty)
                        this.APE.Muscoloskeletal = this.MuscoloSkeletals.First();
                    break;
                case SingleLineEntries.APEInfectiousCommunicable:
                    this.InfectiousCommunicables = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APEInfectiousCommunicable, this.ModuleId, true));
                    if (this.APE != null && this.APE.InfectiousCommunicable != string.Empty)
                        this.APE.InfectiousCommunicable = this.InfectiousCommunicables.First();
                    break;
                case SingleLineEntries.APENeurological:
                    this.Neurologies = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APENeurological, this.ModuleId, true));
                    if (this.APE != null && this.APE.Neurological != string.Empty)
                        this.APE.Neurological = this.Neurologies.First();
                    break;
                case SingleLineEntries.APESurgical:
                    this.Surgicals = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.APESurgical, this.ModuleId, true));
                    if (this.APE != null && this.APE.Surgical != string.Empty)
                        this.APE.Surgical = this.Surgicals.First();
                    break;
                case SingleLineEntries.VitalSignsBy:
                    this.VitalSignsBys = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.VitalSignsBy, this.ModuleId, true));
                    if (this.APE != null && this.APE.VitalSignsBy != string.Empty)
                        this.APE.VitalSignsBy = this.VitalSignsBys.First();
                    break;
                case SingleLineEntries.HeightWeightBy:
                    this.HeightWeightBys = new ObservableCollection<string>(_commonFunctions.LabResultsSingleLineEntryList(SingleLineEntries.HeightWeightBy, this.ModuleId, true));
                    if (this.APE != null && this.APE.HeightWeightBy != string.Empty)
                        this.APE.HeightWeightBy = this.HeightWeightBys.First();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void AddSetDefaultOptions()
        {
            this.TextFields = new ObservableCollection<string>(new List<string>() { Texts.SetDefault });
        }

        private void LoadAllSingleLineEntryLists()
        {
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.Gender);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.CivilStatus);

            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APEENT);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APEGastroenterology);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APERespiratory);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APEIntegumentarySkin);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APECardiology);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APEPsychology);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APEEndocrinology);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APEOBGyneUrology);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APEMuscoloskeletal);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APEInfectiousCommunicable);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APENeurological);
            RefreshLabResultsSingleLineEntryList(SingleLineEntries.APESurgical);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.VitalSignsBy);
            base.RefreshLabResultsSingleLineEntryList(SingleLineEntries.HeightWeightBy);
        }

        private void SetBooleans(APE ape)
        {
            ape.DrinkingDaily = ape.DrinkingFrequency == "Daily";
            ape.DrinkingWeekly = ape.DrinkingFrequency == "Weekly";
            ape.DrinkingOccasional = ape.DrinkingFrequency == "Occasional";
            ape.LMPTypeRegular = ape.LMPType == "Regular";
            ape.LMPTypeIrregular = ape.LMPType == "Irregular";
            ape.VisualAcuityNormal = ape.VisualAcuity == "Normal";
            ape.VisualAcuityEOR = ape.VisualAcuity == "EOR";
            ape.VisualAcuityCorrected = ape.VisualAcuity == "Corrected";

            ape.SkinFValue = PEBooleanValue(ape.Skin, "F");
            ape.SkinNValue = PEBooleanValue(ape.Skin, "N");
            ape.HeadScalpFValue = PEBooleanValue(ape.HeadScalp, "F"); ;
            ape.HeadScalpNValue = PEBooleanValue(ape.HeadScalp, "N");
            ape.EyesFValue = PEBooleanValue(ape.Eyes, "F");
            ape.EyesNValue = PEBooleanValue(ape.Eyes, "N");
            ape.EarsFValue = PEBooleanValue(ape.Ears, "F");
            ape.EarsNValue = PEBooleanValue(ape.Ears, "N");
            ape.NoseFValue = PEBooleanValue(ape.Nose, "F");
            ape.NoseNValue = PEBooleanValue(ape.Nose, "N");
            ape.TTTPFValue = PEBooleanValue(ape.TeethTonsilsThroatPharynx, "F");
            ape.TTTPNValue = PEBooleanValue(ape.TeethTonsilsThroatPharynx, "N");
            ape.NLNTFValue = PEBooleanValue(ape.NeckLymphNodesThyroid, "F");
            ape.NLNTNValue = PEBooleanValue(ape.NeckLymphNodesThyroid, "N");
            ape.TBFValue = PEBooleanValue(ape.ThoraxBreast, "F");
            ape.TBNValue = PEBooleanValue(ape.ThoraxBreast, "N");
            ape.HLFValue = PEBooleanValue(ape.HeartLungs, "F");
            ape.HLNValue = PEBooleanValue(ape.HeartLungs, "N");
            ape.ALSFValue = PEBooleanValue(ape.AbdomenLiverSpleen, "F");
            ape.ALSNValue = PEBooleanValue(ape.AbdomenLiverSpleen, "N");
            ape.IAGAFValue = PEBooleanValue(ape.InguinalAreaGenitalsAnus, "F");
            ape.IAGANValue = PEBooleanValue(ape.InguinalAreaGenitalsAnus, "N");
            ape.ExSFValue = PEBooleanValue(ape.ExtremetiesSpine, "F");
            ape.ExSNValue = PEBooleanValue(ape.ExtremetiesSpine, "N");
            ape.TattooFValue = PEBooleanValue(ape.Tattoo, "F");
            ape.TattooNValue = PEBooleanValue(ape.Tattoo, "N");
            ape.MassCystFValue = PEBooleanValue(ape.MassCyst, "F");
            ape.MassCystNValue = PEBooleanValue(ape.MassCyst, "N");
            ape.OthersPEFValue = PEBooleanValue(ape.OthersPE, "F");
            ape.OthersPENValue = PEBooleanValue(ape.OthersPE, "N");
        }

        private string PEStringValue(bool? nValue, bool? fValue)
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

        private bool PEBooleanValue(string value, string reverseValue)
        {
            return value == null ? false : value.Trim() == reverseValue;
        }
        #endregion
    }
}
