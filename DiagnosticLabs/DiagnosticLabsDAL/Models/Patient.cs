using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class Patient : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private string il_PatientName;
        private DateTime? il_DateOfBirth;
        private string il_Age;
        private string il_Gender;
        private string il_CivilStatus;
        private string il_ContactNumbers;
        private string il_Address;
        private long? il_CompanyId;
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

        public string PatientName
        {
            get { return il_PatientName; }
            set { il_PatientName = value; OnPropertyChanged("PatientName"); }
        }

        public DateTime? DateOfBirth
        {
            get { return il_DateOfBirth; }
            set { il_DateOfBirth = value; OnPropertyChanged("DateOfBirth"); }
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

        public string Address
        {
            get { return il_Address; }
            set { il_Address = value; OnPropertyChanged("Address"); }
        }

        public string ContactNumbers
        {
            get { return il_ContactNumbers; }
            set { il_ContactNumbers = value; OnPropertyChanged("ContactNumbers"); }
        }

        public long? CompanyId
        {
            get { return il_CompanyId; }
            set { il_CompanyId = value; OnPropertyChanged("CompanyId"); }
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

        [NotMapped]
        public bool IsAgeEdited { get; set; } = false;

        #region Validation
        private static readonly string[] PropertiesToValidate = { "PatientName" };

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
                foreach (string property in PropertiesToValidate)
                    if (GetValidationError(property) != string.Empty)
                        errorFound = true;

                return !errorFound;
            }
        }

        private string GetValidationError(string columnName)
        {
            string result = string.Empty;
            if (columnName == "PatientName" && this.PatientName.Trim() == string.Empty)
                result = "Name can not be empty.";

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
