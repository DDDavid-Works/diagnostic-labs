using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class MER : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long? il_PatientId;
        private long? il_PatientRegistrationId;
        private DateTime? il_DateInputted;
        private string il_PatientName;
        private string il_ContactNo;
        private string il_Age;
        private string il_Gender;
        private string il_CivilStatus;
        private string il_CompanyName;
        private string il_ChestXray;
        private string il_ChestXrayRemarks;
        private string il_CBC;
        private string il_CBCRemarks;
        private string il_Urinalysis;
        private string il_UrinalysisRemarks;
        private string il_Fecalysis;
        private string il_FecalysisRemarks;
        private string il_HBsAg;
        private string il_HBsAgRemarks;
        private string il_DrugTest2Panel;
        private string il_DrugTest2PanelRemarks;
        private string il_DrugTest4Panel;
        private string il_DrugTest4PanelRemarks;
        private string il_Classification;
        private string il_MedicalSurgicalHistory;
        private string il_Assessment;
        private string il_Remarks;
        private string il_AssessmentDoneBy;
        private string il_PhysicianName;
        private string il_PhysicianLicense;
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

        public string ContactNo
        {
            get { return il_ContactNo; }
            set { il_ContactNo = value; OnPropertyChanged("ContactNo"); }
        }

        public string Age
        {
            get { return il_Age; }
            set { il_Age = value; OnPropertyChanged("Age"); }
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

        public string CompanyName
        {
            get { return il_CompanyName; }
            set { il_CompanyName = value; OnPropertyChanged("CompanyName"); }
        }

        public string ChestXray
        {
            get { return il_ChestXray; }
            set { il_ChestXray = value; OnPropertyChanged("ChestXray"); }
        }

        public string ChestXrayRemarks
        {
            get { return il_ChestXrayRemarks; }
            set { il_ChestXrayRemarks = value; OnPropertyChanged("ChestXrayRemarks"); }
        }

        public string CBC
        {
            get { return il_CBC; }
            set { il_CBC = value; OnPropertyChanged("CBC"); }
        }

        public string CBCRemarks
        {
            get { return il_CBCRemarks; }
            set { il_CBCRemarks = value; OnPropertyChanged("CBCRemarks"); }
        }

        public string Urinalysis
        {
            get { return il_Urinalysis; }
            set { il_Urinalysis = value; OnPropertyChanged("Urinalysis"); }
        }

        public string UrinalysisRemarks
        {
            get { return il_UrinalysisRemarks; }
            set { il_UrinalysisRemarks = value; OnPropertyChanged("UrinalysisRemarks"); }
        }

        public string Fecalysis
        {
            get { return il_Fecalysis; }
            set { il_Fecalysis = value; OnPropertyChanged("Fecalysis"); }
        }

        public string FecalysisRemarks
        {
            get { return il_FecalysisRemarks; }
            set { il_FecalysisRemarks = value; OnPropertyChanged("FecalysisRemarks"); }
        }

        public string HBsAg
        {
            get { return il_HBsAg; }
            set { il_HBsAg = value; OnPropertyChanged("HBsAg"); }
        }

        public string HBsAgRemarks
        {
            get { return il_HBsAgRemarks; }
            set { il_HBsAgRemarks = value; OnPropertyChanged("HBsAgRemarks"); }
        }

        public string DrugTest2Panel
        {
            get { return il_DrugTest2Panel; }
            set { il_DrugTest2Panel = value; OnPropertyChanged("DrugTest2Panel"); }
        }

        public string DrugTest2PanelRemarks
        {
            get { return il_DrugTest2PanelRemarks; }
            set { il_DrugTest2PanelRemarks = value; OnPropertyChanged("DrugTest2PanelRemarks"); }
        }

        public string DrugTest4Panel
        {
            get { return il_DrugTest4Panel; }
            set { il_DrugTest4Panel = value; OnPropertyChanged("DrugTest4Panel"); }
        }

        public string DrugTest4PanelRemarks
        {
            get { return il_DrugTest4PanelRemarks; }
            set { il_DrugTest4PanelRemarks = value; OnPropertyChanged("DrugTest4PanelRemarks"); }
        }

        public string Classification
        {
            get { return il_Classification; }
            set { il_Classification = value; OnPropertyChanged("Classification"); }
        }

        public string MedicalSurgicalHistory
        {
            get { return il_MedicalSurgicalHistory; }
            set { il_MedicalSurgicalHistory = value; OnPropertyChanged("MedicalSurgicalHistory"); }
        }

        public string Assessment
        {
            get { return il_Assessment; }
            set { il_Assessment = value; OnPropertyChanged("Assessment"); }
        }

        public string Remarks
        {
            get { return il_Remarks; }
            set { il_Remarks = value; OnPropertyChanged("Remarks"); }
        }

        public string AssessmentDoneBy
        {
            get { return il_AssessmentDoneBy; }
            set { il_AssessmentDoneBy = value; OnPropertyChanged("AssessmentDoneBy"); }
        }

        public string PhysicianName
        {
            get { return il_PhysicianName; }
            set { il_PhysicianName = value; OnPropertyChanged("PhysicianName"); }
        }

        public string PhysicianLicense
        {
            get { return il_PhysicianLicense; }
            set { il_PhysicianLicense = value; OnPropertyChanged("PhysicianLicense "); }
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
        public bool ChestXRayNValue { get; set; }

        [NotMapped]
        public bool ChestXRayFValue { get; set; }

        [NotMapped]
        public bool CBCNValue { get; set; }

        [NotMapped]
        public bool CBCFValue { get; set; }

        [NotMapped]
        public bool UrinalysisNValue { get; set; }

        [NotMapped]
        public bool UrinalysisFValue { get; set; }

        [NotMapped]
        public bool FecalysisNValue { get; set; }

        [NotMapped]
        public bool FecalysisFValue { get; set; }

        [NotMapped]
        public bool HBsAgNValue { get; set; }

        [NotMapped]
        public bool HBsAgFValue { get; set; }

        [NotMapped]
        public bool DrugTest2PanelNValue { get; set; }

        [NotMapped]
        public bool DrugTest2PanelFValue { get; set; }

        [NotMapped]
        public bool DrugTest4PanelNValue { get; set; }

        [NotMapped]
        public bool DrugTest4PanelFValue { get; set; }
        #endregion

        #region Validation
        private static readonly string[] _propertiesToValidate = { };

        public string Error
        {
            get
            {
                if (ErrorMessages != null)
                    return ErrorMessages.Trim();
                else
                    return string.Empty;
            }
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
