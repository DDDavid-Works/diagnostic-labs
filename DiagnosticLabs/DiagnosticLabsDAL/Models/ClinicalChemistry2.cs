using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class ClinicalChemistry2 : BaseModel, IDataErrorInfo
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
        private string il_AlkalinePhosphataseCNValue;
        private string il_AlkalinePhosphataseCUnit;
        private string il_AlkalinePhosphataseCResults;
        private string il_AlkalinePhosphataseSNValue;
        private string il_AlkalinePhosphataseSUnit;
        private string il_AlkalinePhosphataseSResults;
        private string il_SGOTCNValue;
        private string il_SGOTCUnit;
        private string il_SGOTCResults;
        private string il_SGOTSNValue;
        private string il_SGOTSUnit;
        private string il_SGOTSResults;
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

        public string AlkalinePhosphataseCNValue
        {
            get { return il_AlkalinePhosphataseCNValue; }
            set { il_AlkalinePhosphataseCNValue = value; OnPropertyChanged("AlkalinePhosphataseCNValue"); }
        }

        public string AlkalinePhosphataseCUnit
        {
            get { return il_AlkalinePhosphataseCUnit; }
            set { il_AlkalinePhosphataseCUnit = value; OnPropertyChanged("AlkalinePhosphataseCUnit"); }
        }

        public string AlkalinePhosphataseCResults
        {
            get { return il_AlkalinePhosphataseCResults; }
            set { il_AlkalinePhosphataseCResults = value; OnPropertyChanged("AlkalinePhosphataseCResults"); }
        }

        public string AlkalinePhosphataseSNValue
        {
            get { return il_AlkalinePhosphataseSNValue; }
            set { il_AlkalinePhosphataseSNValue = value; OnPropertyChanged("AlkalinePhosphataseSNValue"); }
        }

        public string AlkalinePhosphataseSUnit
        {
            get { return il_AlkalinePhosphataseSUnit; }
            set { il_AlkalinePhosphataseSUnit = value; OnPropertyChanged("AlkalinePhosphataseSUnit"); }
        }

        public string AlkalinePhosphataseSResults
        {
            get { return il_AlkalinePhosphataseSResults; }
            set { il_AlkalinePhosphataseSResults = value; OnPropertyChanged("AlkalinePhosphataseSResults"); }
        }

        public string SGOTCNValue
        {
            get { return il_SGOTCNValue; }
            set { il_SGOTCNValue = value; OnPropertyChanged("SGOTCNValue"); }
        }

        public string SGOTCUnit
        {
            get { return il_SGOTCUnit; }
            set { il_SGOTCUnit = value; OnPropertyChanged("SGOTCUnit"); }
        }

        public string SGOTCResults
        {
            get { return il_SGOTCResults; }
            set { il_SGOTCResults = value; OnPropertyChanged("SGOTCResults"); }
        }

        public string SGOTSNValue
        {
            get { return il_SGOTSNValue; }
            set { il_SGOTSNValue = value; OnPropertyChanged("SGOTSNValue"); }
        }

        public string SGOTSUnit
        {
            get { return il_SGOTSUnit; }
            set { il_SGOTSUnit = value; OnPropertyChanged("SGOTSUnit"); }
        }

        public string SGOTSResults
        {
            get { return il_SGOTSResults; }
            set { il_SGOTSResults = value; OnPropertyChanged("SGOTSResults"); }
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
                return !errorFound;
            }
        }

        private string GetValidationError(string columnName)
        {
            string result = string.Empty;
            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');
            return result;
        }
        #endregion
    }
}
