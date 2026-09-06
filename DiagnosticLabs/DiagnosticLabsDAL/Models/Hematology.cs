using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class Hematology : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long? il_PatientId;
        private long? il_PatientRegistrationId;
        private string is_PatientCode;
        private string is_PatientName;
        private string is_CompanyOrPhysician;
        private string is_Age;
        private string is_Sex;
        private DateTime? id_DateRequested;
        private byte[] ibyte_Photo;
        private string is_HematocritNValue;
        private string is_HematocritResult;
        private string is_HemoglobinNValue;
        private string is_HemoglobinResult;
        private string is_WBCCountNValue;
        private string is_WBCCountResult;
        private string is_SegmentersNValue;
        private string is_SegmentersResult;
        private string is_LymphocytesNValue;
        private string is_LymphocytesResult;
        private string is_EosinophilsNValue;
        private string is_EosinophilsResult;
        private string is_MonocytesNValue;
        private string is_MonocytesResult;
        private string is_BasophilsNValue;
        private string is_BasophilsResult;
        private string is_StabNValue;
        private string is_StabResult;
        private string is_PlateletCountNValue;
        private string is_PlateletCountResult;
        private string is_Remarks;
        private string is_MedicalTechnologist;
        private string is_Pathologist;
        private bool ib_IsActive;
        private long il_CreatedByUserId;
        private DateTime id_CreatedDate;
        private long il_UpdatedByUserId;
        private DateTime id_UpdatedDate;

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
            get { return is_PatientCode; }
            set { is_PatientCode = value; OnPropertyChanged("PatientCode"); }
        }

        public string PatientName
        {
            get { return is_PatientName; }
            set { is_PatientName = value; OnPropertyChanged("PatientName"); }
        }

        public string CompanyOrPhysician
        {
            get { return is_CompanyOrPhysician; }
            set { is_CompanyOrPhysician = value; OnPropertyChanged("CompanyOrPhysician"); }
        }

        public string Age
        {
            get { return is_Age; }
            set { is_Age = value; OnPropertyChanged("Age"); }
        }

        public string Sex
        {
            get { return is_Sex; }
            set { is_Sex = value; OnPropertyChanged("Sex"); }
        }

        public DateTime? DateRequested
        {
            get { return id_DateRequested; }
            set { id_DateRequested = value; OnPropertyChanged("DateRequested"); }
        }

        public byte[] Photo
        {
            get { return ibyte_Photo; }
            set { ibyte_Photo = value; OnPropertyChanged("Photo"); }
        }

        public string HematocritNValue
        {
            get { return is_HematocritNValue; }
            set { is_HematocritNValue = value; OnPropertyChanged("HematocritNValue"); }
        }

        public string HematocritResult
        {
            get { return is_HematocritResult; }
            set { is_HematocritResult = value; OnPropertyChanged("HematocritResult"); }
        }

        public string HemoglobinNValue
        {
            get { return is_HemoglobinNValue; }
            set { is_HemoglobinNValue = value; OnPropertyChanged("HemoglobinNValue"); }
        }

        public string HemoglobinResult
        {
            get { return is_HemoglobinResult; }
            set { is_HemoglobinResult = value; OnPropertyChanged("HemoglobinResult"); }
        }

        public string WBCCountNValue
        {
            get { return is_WBCCountNValue; }
            set { is_WBCCountNValue = value; OnPropertyChanged("WBCCountNValue"); }
        }

        public string WBCCountResult
        {
            get { return is_WBCCountResult; }
            set { is_WBCCountResult = value; OnPropertyChanged("WBCCountResult"); }
        }

        public string SegmentersNValue
        {
            get { return is_SegmentersNValue; }
            set { is_SegmentersNValue = value; OnPropertyChanged("SegmentersNValue"); }
        }

        public string SegmentersResult
        {
            get { return is_SegmentersResult; }
            set { is_SegmentersResult = value; OnPropertyChanged("SegmentersResult"); }
        }

        public string LymphocytesNValue
        {
            get { return is_LymphocytesNValue; }
            set { is_LymphocytesNValue = value; OnPropertyChanged("LymphocytesNValue"); }
        }

        public string LymphocytesResult
        {
            get { return is_LymphocytesResult; }
            set { is_LymphocytesResult = value; OnPropertyChanged("LymphocytesResult"); }
        }

        public string EosinophilsNValue
        {
            get { return is_EosinophilsNValue; }
            set { is_EosinophilsNValue = value; OnPropertyChanged("EosinophilsNValue"); }
        }

        public string EosinophilsResult
        {
            get { return is_EosinophilsResult; }
            set { is_EosinophilsResult = value; OnPropertyChanged("EosinophilsResult"); }
        }

        public string MonocytesNValue
        {
            get { return is_MonocytesNValue; }
            set { is_MonocytesNValue = value; OnPropertyChanged("MonocytesNValue"); }
        }

        public string MonocytesResult
        {
            get { return is_MonocytesResult; }
            set { is_MonocytesResult = value; OnPropertyChanged("MonocytesResult"); }
        }

        public string BasophilsNValue
        {
            get { return is_BasophilsNValue; }
            set { is_BasophilsNValue = value; OnPropertyChanged("BasophilsNValue"); }
        }

        public string BasophilsResult
        {
            get { return is_BasophilsResult; }
            set { is_BasophilsResult = value; OnPropertyChanged("BasophilsResult"); }
        }

        public string StabNValue
        {
            get { return is_StabNValue; }
            set { is_StabNValue = value; OnPropertyChanged("StabNValue"); }
        }

        public string StabResult
        {
            get { return is_StabResult; }
            set { is_StabResult = value; OnPropertyChanged("StabResult"); }
        }

        public string PlateletCountNValue
        {
            get { return is_PlateletCountNValue; }
            set { is_PlateletCountNValue = value; OnPropertyChanged("PlateletCountNValue"); }
        }

        public string PlateletCountResult
        {
            get { return is_PlateletCountResult; }
            set { is_PlateletCountResult = value; OnPropertyChanged("PlateletCountResult"); }
        }

        public string Remarks
        {
            get { return is_Remarks; }
            set { is_Remarks = value; OnPropertyChanged("Remarks"); }
        }

        public string MedicalTechnologist
        {
            get { return is_MedicalTechnologist; }
            set { is_MedicalTechnologist = value; OnPropertyChanged("MedicalTechnologist"); }
        }

        public string Pathologist
        {
            get { return is_Pathologist; }
            set { is_Pathologist = value; OnPropertyChanged("Pathologist"); }
        }

        public bool IsActive
        {
            get { return ib_IsActive; }
            set { ib_IsActive = value; OnPropertyChanged("IsActive"); }
        }

        public long CreatedByUserId
        {
            get { return il_CreatedByUserId; }
            set { il_CreatedByUserId = value; OnPropertyChanged("CreatedByUserId"); }
        }

        public DateTime CreatedDate
        {
            get { return id_CreatedDate; }
            set { id_CreatedDate = value; OnPropertyChanged("CreatedDate"); }
        }

        public long UpdatedByUserId
        {
            get { return il_UpdatedByUserId; }
            set { il_UpdatedByUserId = value; OnPropertyChanged("UpdatedByUserId"); }
        }

        public DateTime UpdatedDate
        {
            get { return id_UpdatedDate; }
            set { id_UpdatedDate = value; OnPropertyChanged("UpdatedDate"); }
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