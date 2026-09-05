using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class ClinicalChemistry : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long? il_PatientId;
        private long? il_PatientRegistrationId;
        private string il_PatientCode;
        private string il_PatientName;
        private string il_CompanyOrPhysician;
        private string il_Age;
        private string il_Sex;
        private DateTime? il_DateRequested;
        private byte[] il_Photo;
        private string il_FBSNValue;
        private string il_FBSResult;
        private string il_TotalCholesterolNValue;
        private string il_TotalCholesterolResult;
        private string il_TriglyceridesNValue;
        private string il_TriglyceridesResult;
        private string il_HDLNValue;
        private string il_HDLResult;
        private string il_BUNNValue;
        private string il_BUNResult;
        private string il_CreatinineNValue;
        private string il_CreatinineResult;
        private string il_BloodUricAcidNValue;
        private string il_BloodUricAcidResult;
        private string il_LDLNValue;
        private string il_LDLResult;
        private string il_SGPTNValue;
        private string il_SGPTResult;
        private string il_MedicalTechnologist;
        private string il_Pathologist;
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

        public string PatientCode
        {
            get { return il_PatientCode; }
            set { il_PatientCode = value; OnPropertyChanged("PatientCode"); }
        }

        public string PatientName
        {
            get { return il_PatientName; }
            set { il_PatientName = value; OnPropertyChanged("PatientName"); }
        }

        public string CompanyOrPhysician
        {
            get { return il_CompanyOrPhysician; }
            set { il_CompanyOrPhysician = value; OnPropertyChanged("CompanyOrPhysician"); }
        }

        public string Age
        {
            get { return il_Age; }
            set { il_Age = value; OnPropertyChanged("Age"); }
        }

        public string Sex
        {
            get { return il_Sex; }
            set { il_Sex = value; OnPropertyChanged("Sex"); }
        }

        public DateTime? DateRequested
        {
            get { return il_DateRequested; }
            set { il_DateRequested = value; OnPropertyChanged("DateRequested"); }
        }

        public byte[] Photo
        {
            get { return il_Photo; }
            set { il_Photo = value; OnPropertyChanged("Photo"); }
        }

        public string FBSNValue
        {
            get { return il_FBSNValue; }
            set { il_FBSNValue = value; OnPropertyChanged("FBSNValue"); }
        }

        public string FBSResult
        {
            get { return il_FBSResult; }
            set { il_FBSResult = value; OnPropertyChanged("FBSResult"); }
        }

        public string TotalCholesterolNValue
        {
            get { return il_TotalCholesterolNValue; }
            set { il_TotalCholesterolNValue = value; OnPropertyChanged("TotalCholesterolNValue"); }
        }

        public string TotalCholesterolResult
        {
            get { return il_TotalCholesterolResult; }
            set { il_TotalCholesterolResult = value; OnPropertyChanged("TotalCholesterolResult"); }
        }

        public string TriglyceridesNValue
        {
            get { return il_TriglyceridesNValue; }
            set { il_TriglyceridesNValue = value; OnPropertyChanged("TriglyceridesNValue"); }
        }

        public string TriglyceridesResult
        {
            get { return il_TriglyceridesResult; }
            set { il_TriglyceridesResult = value; OnPropertyChanged("TriglyceridesResult"); }
        }

        public string HDLNValue
        {
            get { return il_HDLNValue; }
            set { il_HDLNValue = value; OnPropertyChanged("HDLNValue"); }
        }

        public string HDLResult
        {
            get { return il_HDLResult; }
            set { il_HDLResult = value; OnPropertyChanged("HDLResult"); }
        }

        public string BUNNValue
        {
            get { return il_BUNNValue; }
            set { il_BUNNValue = value; OnPropertyChanged("BUNNValue"); }
        }

        public string BUNResult
        {
            get { return il_BUNResult; }
            set { il_BUNResult = value; OnPropertyChanged("BUNResult"); }
        }

        public string CreatinineNValue
        {
            get { return il_CreatinineNValue; }
            set { il_CreatinineNValue = value; OnPropertyChanged("CreatinineNValue"); }
        }

        public string CreatinineResult
        {
            get { return il_CreatinineResult; }
            set { il_CreatinineResult = value; OnPropertyChanged("CreatinineResult"); }
        }

        public string BloodUricAcidNValue
        {
            get { return il_BloodUricAcidNValue; }
            set { il_BloodUricAcidNValue = value; OnPropertyChanged("BloodUricAcidNValue"); }
        }

        public string BloodUricAcidResult
        {
            get { return il_BloodUricAcidResult; }
            set { il_BloodUricAcidResult = value; OnPropertyChanged("BloodUricAcidResult"); }
        }

        public string LDLNValue
        {
            get { return il_LDLNValue; }
            set { il_LDLNValue = value; OnPropertyChanged("LDLNValue"); }
        }

        public string LDLResult
        {
            get { return il_LDLResult; }
            set { il_LDLResult = value; OnPropertyChanged("LDLResult"); }
        }

        public string SGPTNValue
        {
            get { return il_SGPTNValue; }
            set { il_SGPTNValue = value; OnPropertyChanged("SGPTNValue"); }
        }

        public string SGPTResult
        {
            get { return il_SGPTResult; }
            set { il_SGPTResult = value; OnPropertyChanged("SGPTResult"); }
        }

        public string MedicalTechnologist
        {
            get { return il_MedicalTechnologist; }
            set { il_MedicalTechnologist = value; OnPropertyChanged("MedicalTechnologist"); }
        }

        public string Pathologist
        {
            get { return il_Pathologist; }
            set { il_Pathologist = value; OnPropertyChanged("Pathologist"); }
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
