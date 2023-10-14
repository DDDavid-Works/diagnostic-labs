using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class APE : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long? il_PatientId;
        private long? il_PatientRegistrationId;
        private DateTime? il_DateInputted;
        private string il_PatientName;
        private string il_CompanyName;
        private string il_DepartmentOrAgency;
        private string il_Age;
        private DateTime? il_BirthDate;
        private string il_Gender;
        private string il_CivilStatus;
        private string il_ContactNo;
        private string il_ENT;
        private string il_Gastroenterology;
        private string il_Respiratory;
        private string il_IntegumentarySkin;
        private string il_Cardiology;
        private string il_Psychology;
        private string il_Endocrinology;
        private string il_OBGyneUrology;
        private string il_Muscoloskeletal;
        private string il_InfectiousCommunicable;
        private string il_Neurological;
        private string il_Surgical;
        private string il_OthersPast;
        private string il_Medications;
        private string il_ReviewOfSystems;
        private string il_Allergies;
        private bool il_IsSmoking;
        private string il_SmokingSinceWhen;
        private int? il_NumberOfSticksPerDay;
        private bool il_IsDrinking;
        private string il_DrinkingSinceWhen;
        private int? il_NumberOfBottles;
        private string il_DrinkingFrequency;
        private string il_LMP;
        private string il_LMPType;
        private string il_BP1st;
        private string il_BP2nd;
        private string il_CardiacRate1st;
        private string il_CardiacRate2nd;
        private string il_Height;
        private string il_Weight;
        private string il_BMICategory;
        private string il_VARightEyeWGlasses;
        private string il_VARightEyeWOGlasses;
        private string il_VALeftEyeWGlasses;
        private string il_VALeftEyeWOGlasses;
        private string il_VisualAcuity;
        private string il_Skin;
        private string il_HeadScalp;
        private string il_Eyes;
        private string il_Ears;
        private string il_Nose;
        private string il_TeethTonsilsThroatPharynx;
        private string il_NeckLymphNodesThyroid;
        private string il_ThoraxBreast;
        private string il_HeartLungs;
        private string il_AbdomenLiverSpleen;
        private string il_InguinalAreaGenitalsAnus;
        private string il_ExtremetiesSpine;
        private string il_Tattoo;
        private string il_MassCyst;
        private string il_OthersPE;
        private string il_Findings;
        private string il_VitalSignsBy;
        private string il_HeightWeightBy;
        private bool il_IsActive;
        private long il_CreatedByUserId;
        private DateTime il_CreatedDate;
        private long il_UpdatedByUserId;
        private DateTime il_UpdatedDate;

        [Key]
        public long Id
        {
            get { return il_Id; }
            set { il_Id = value; OnPropertyChanged("Id"); }
        }

        public long? PatientId
        {
            get { return il_PatientId; }
            set { il_PatientId = value; OnPropertyChanged("PatientId"); }
        }

        public long? PatientRegistrationId
        {
            get { return il_PatientRegistrationId; }
            set { il_PatientRegistrationId = value; OnPropertyChanged("PatientRegistrationId"); }
        }

        public DateTime? DateInputted
        {
            get { return il_DateInputted; }
            set { il_DateInputted = value; OnPropertyChanged("DateInputted"); }
        }

        public string PatientName
        {
            get { return il_PatientName; }
            set { il_PatientName = value; OnPropertyChanged("PatientName"); }
        }

        public string CompanyName
        {
            get { return il_CompanyName; }
            set { il_CompanyName = value; OnPropertyChanged("CompanyName"); }
        }

        public string DepartmentOrAgency
        {
            get { return il_DepartmentOrAgency; }
            set { il_DepartmentOrAgency = value; OnPropertyChanged("DepartmentOrAgency"); }
        }

        public string Age
        {
            get { return il_Age; }
            set { il_Age = value; OnPropertyChanged("Age"); }
        }

        public DateTime? BirthDate
        {
            get { return il_BirthDate; }
            set { il_BirthDate = value; OnPropertyChanged("BirthDate"); }
        }

        public string Gender
        {
            get { return il_Gender; }
            set { il_Gender = value; OnPropertyChanged("Gender"); }
        }

        public string CivilStatus
        {
            get { return il_CivilStatus; }
            set { il_CivilStatus = value; OnPropertyChanged("CivilStatus"); }
        }

        public string ContactNo
        {
            get { return il_ContactNo; }
            set { il_ContactNo = value; OnPropertyChanged("ContactNo"); }
        }

        public string ENT
        {
            get { return il_ENT; }
            set { il_ENT = value; OnPropertyChanged("ENT"); }
        }

        public string Gastroenterology
        {
            get { return il_Gastroenterology; }
            set { il_Gastroenterology = value; OnPropertyChanged("Gastroenterology"); }
        }

        public string Respiratory
        {
            get { return il_Respiratory; }
            set { il_Respiratory = value; OnPropertyChanged("Respiratory"); }
        }

        public string IntegumentarySkin
        {
            get { return il_IntegumentarySkin; }
            set { il_IntegumentarySkin = value; OnPropertyChanged("IntegumentarySkin"); }
        }

        public string Cardiology
        {
            get { return il_Cardiology; }
            set { il_Cardiology = value; OnPropertyChanged("Cardiology"); }
        }

        public string Psychology
        {
            get { return il_Psychology; }
            set { il_Psychology = value; OnPropertyChanged("Psychology"); }
        }

        public string Endocrinology
        {
            get { return il_Endocrinology; }
            set { il_Endocrinology = value; OnPropertyChanged("Endocrinology"); }
        }

        public string OBGyneUrology
        {
            get { return il_OBGyneUrology; }
            set { il_OBGyneUrology = value; OnPropertyChanged("OBGyneUrology"); }
        }

        public string Muscoloskeletal
        {
            get { return il_Muscoloskeletal; }
            set { il_Muscoloskeletal = value; OnPropertyChanged("Muscoloskeletal"); }
        }

        public string InfectiousCommunicable
        {
            get { return il_InfectiousCommunicable; }
            set { il_InfectiousCommunicable = value; OnPropertyChanged("InfectiousCommunicable"); }
        }

        public string Neurological
        {
            get { return il_Neurological; }
            set { il_Neurological = value; OnPropertyChanged("Neurological"); }
        }

        public string Surgical
        {
            get { return il_Surgical; }
            set { il_Surgical = value; OnPropertyChanged("Surgical"); }
        }

        public string OthersPast
        {
            get { return il_OthersPast; }
            set { il_OthersPast = value; OnPropertyChanged("OthersPast"); }
        }

        public string Medications
        {
            get { return il_Medications; }
            set { il_Medications = value; OnPropertyChanged("Medications"); }
        }

        public string ReviewOfSystems
        {
            get { return il_ReviewOfSystems; }
            set { il_ReviewOfSystems = value; OnPropertyChanged("ReviewOfSystems"); }
        }

        public string Allergies
        {
            get { return il_Allergies; }
            set { il_Allergies = value; OnPropertyChanged("Allergies"); }
        }

        public bool IsSmoking
        {
            get { return il_IsSmoking; }
            set { il_IsSmoking = value; OnPropertyChanged("IsSmoking"); }
        }

        public string SmokingSinceWhen
        {
            get { return il_SmokingSinceWhen; }
            set { il_SmokingSinceWhen = value; OnPropertyChanged("SmokingSinceWhen"); }
        }

        public int? NumberOfSticksPerDay
        {
            get { return il_NumberOfSticksPerDay; }
            set { il_NumberOfSticksPerDay = value; OnPropertyChanged("NumberOfSticksPerDay"); }
        }

        public bool IsDrinking
        {
            get { return il_IsDrinking; }
            set { il_IsDrinking = value; OnPropertyChanged("IsDrinking"); }
        }

        public string DrinkingSinceWhen
        {
            get { return il_DrinkingSinceWhen; }
            set { il_DrinkingSinceWhen = value; OnPropertyChanged("DrinkingSinceWhen"); }
        }

        public int? NumberOfBottles
        {
            get { return il_NumberOfBottles; }
            set { il_NumberOfBottles = value; OnPropertyChanged("NumberOfBottles"); }
        }

        public string DrinkingFrequency
        {
            get { return il_DrinkingFrequency; }
            set { il_DrinkingFrequency = value; OnPropertyChanged("DrinkingFrequency"); }
        }

        public string LMP
        {
            get { return il_LMP; }
            set { il_LMP = value; OnPropertyChanged("LMP"); }
        }

        public string LMPType
        {
            get { return il_LMPType; }
            set { il_LMPType = value; OnPropertyChanged("LMPType"); }
        }

        public string BP1st
        {
            get { return il_BP1st; }
            set { il_BP1st = value; OnPropertyChanged("BP1st"); }
        }

        public string BP2nd
        {
            get { return il_BP2nd; }
            set { il_BP2nd = value; OnPropertyChanged("BP2nd"); }
        }

        public string CardiacRate1st
        {
            get { return il_CardiacRate1st; }
            set { il_CardiacRate1st = value; OnPropertyChanged("CardiacRate1st"); }
        }

        public string CardiacRate2nd
        {
            get { return il_CardiacRate2nd; }
            set { il_CardiacRate2nd = value; OnPropertyChanged("CardiacRate2nd"); }
        }

        public string Height
        {
            get { return il_Height; }
            set { il_Height = value; OnPropertyChanged("Height"); }
        }

        public string Weight
        {
            get { return il_Weight; }
            set { il_Weight = value; OnPropertyChanged("Weight"); }
        }

        public string BMICategory
        {
            get { return il_BMICategory; }
            set { il_BMICategory = value; OnPropertyChanged("BMICategory"); }
        }

        public string VARightEyeWGlasses
        {
            get { return il_VARightEyeWGlasses; }
            set { il_VARightEyeWGlasses = value; OnPropertyChanged("VARightEyeWGlasses"); }
        }

        public string VARightEyeWOGlasses
        {
            get { return il_VARightEyeWOGlasses; }
            set { il_VARightEyeWOGlasses = value; OnPropertyChanged("VARightEyeWOGlasses"); }
        }

        public string VALeftEyeWGlasses
        {
            get { return il_VALeftEyeWGlasses; }
            set { il_VALeftEyeWGlasses = value; OnPropertyChanged("VALeftEyeWGlasses"); }
        }

        public string VALeftEyeWOGlasses
        {
            get { return il_VALeftEyeWOGlasses; }
            set { il_VALeftEyeWOGlasses = value; OnPropertyChanged("VALeftEyeWOGlasses"); }
        }

        public string VisualAcuity
        {
            get { return il_VisualAcuity; }
            set { il_VisualAcuity = value; OnPropertyChanged("VisualAcuity"); }
        }

        public string Skin
        {
            get { return il_Skin; }
            set { il_Skin = value; OnPropertyChanged("Skin"); }
        }

        public string HeadScalp
        {
            get { return il_HeadScalp; }
            set { il_HeadScalp = value; OnPropertyChanged("HeadScalp"); }
        }

        public string Eyes
        {
            get { return il_Eyes; }
            set { il_Eyes = value; OnPropertyChanged("Eyes"); }
        }

        public string Ears
        {
            get { return il_Ears; }
            set { il_Ears = value; OnPropertyChanged("Ears"); }
        }

        public string Nose
        {
            get { return il_Nose; }
            set { il_Nose = value; OnPropertyChanged("Nose"); }
        }

        public string TeethTonsilsThroatPharynx
        {
            get { return il_TeethTonsilsThroatPharynx; }
            set { il_TeethTonsilsThroatPharynx = value; OnPropertyChanged("TeethTonsilsThroatPharynx"); }
        }

        public string NeckLymphNodesThyroid
        {
            get { return il_NeckLymphNodesThyroid; }
            set { il_NeckLymphNodesThyroid = value; OnPropertyChanged("NeckLymphNodesThyroid"); }
        }

        public string ThoraxBreast
        {
            get { return il_ThoraxBreast; }
            set { il_ThoraxBreast = value; OnPropertyChanged("ThoraxBreast"); }
        }

        public string HeartLungs
        {
            get { return il_HeartLungs; }
            set { il_HeartLungs = value; OnPropertyChanged("HeartLungs"); }
        }

        public string AbdomenLiverSpleen
        {
            get { return il_AbdomenLiverSpleen; }
            set { il_AbdomenLiverSpleen = value; OnPropertyChanged("AbdomenLiverSpleen"); }
        }

        public string InguinalAreaGenitalsAnus
        {
            get { return il_InguinalAreaGenitalsAnus; }
            set { il_InguinalAreaGenitalsAnus = value; OnPropertyChanged("InguinalAreaGenitalsAnus"); }
        }

        public string ExtremetiesSpine
        {
            get { return il_ExtremetiesSpine; }
            set { il_ExtremetiesSpine = value; OnPropertyChanged("ExtremetiesSpine"); }
        }

        public string Tattoo
        {
            get { return il_Tattoo; }
            set { il_Tattoo = value; OnPropertyChanged("Tattoo"); }
        }

        public string MassCyst
        {
            get { return il_MassCyst; }
            set { il_MassCyst = value; OnPropertyChanged("MassCyst"); }
        }

        public string OthersPE
        {
            get { return il_OthersPE; }
            set { il_OthersPE = value; OnPropertyChanged("OthersPE"); }
        }

        public string Findings
        {
            get { return il_Findings; }
            set { il_Findings = value; OnPropertyChanged("Findings"); }
        }

        public string VitalSignsBy
        {
            get { return il_VitalSignsBy; }
            set { il_VitalSignsBy = value; OnPropertyChanged("VitalSignsBy"); }
        }

        public string HeightWeightBy
        {
            get { return il_HeightWeightBy; }
            set { il_HeightWeightBy = value; OnPropertyChanged("HeightWeightBy"); }
        }

        public bool IsActive
        {
            get { return il_IsActive; }
            set { il_IsActive = value; OnPropertyChanged("IsActive"); }
        }

        public long CreatedByUserId
        {
            get { return il_CreatedByUserId; }
            set { il_CreatedByUserId = value; OnPropertyChanged("CreatedByUserId"); }
        }

        public DateTime CreatedDate
        {
            get { return il_CreatedDate; }
            set { il_CreatedDate = value; OnPropertyChanged("CreatedDate"); }
        }

        public long UpdatedByUserId
        {
            get { return il_UpdatedByUserId; }
            set { il_UpdatedByUserId = value; OnPropertyChanged("UpdatedByUserId"); }
        }

        public DateTime UpdatedDate
        {
            get { return il_UpdatedDate; }
            set { il_UpdatedDate = value; OnPropertyChanged("UpdatedDate"); }
        }

        #region Checkboxes Field
        [NotMapped]
        public string APENumberOfSticksPerDay { get; set; }

        [NotMapped]
        public string APENumberOfBottles { get; set; }

        [NotMapped]
        public bool DrinkingDaily { get; set; }

        [NotMapped]
        public bool DrinkingWeekly { get; set; }

        [NotMapped]
        public bool DrinkingOccasional { get; set; }

        [NotMapped]
        public bool LMPTypeRegular { get; set; }

        [NotMapped]
        public bool LMPTypeIrregular { get; set; }

        [NotMapped]
        public bool VisualAcuityNormal { get; set; }
        [NotMapped]

        public bool VisualAcuityEOR { get; set; }
        [NotMapped]
        public bool VisualAcuityCorrected { get; set; }
        #endregion

        #region Validation
        private static readonly string[] _propertiesToValidate = { };

        public string Error
        {
            get { return ErrorMessages.Trim(); }
        }

        public string this[string columnName]
        {
            get { return !ValidateOnChange ? string.Empty : GetValidationError(columnName); }
        }

        public bool IsValid
        {
            get
            {
                ErrorMessages = string.Empty;

                bool errorFound = false;
                //foreach (string property in _propertiesToValidate)
                //    if (GetValidationError(property) != string.Empty)
                //        errorFound = true;

                return !errorFound;
            }
        }

        private string GetValidationError(string columnName)
        {
            string result = string.Empty;

            //if (columnName == "PatientRegistrationAmountDue")
            //{
            //    decimal patientRegistrationPrice = 0;
            //    bool isDecimal = decimal.TryParse(this.PatientRegistrationAmountDue, out patientRegistrationPrice);
            //    if (!isDecimal)
            //        result = "\r\nPrice is invalid.";
            //}

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
