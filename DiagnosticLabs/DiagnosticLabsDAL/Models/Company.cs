using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class Company : BaseModel
    {
        private long il_Id;
        private string il_CompanyName;
        private string il_Address;
        private string il_ContactNumbers;
        private string il_ContactPerson;
        private bool il_IsSystem;
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

        public string CompanyName
        {
            get { return il_CompanyName; }
            set { il_CompanyName = value; OnPropertyChanged("CompanyName"); }
        }

        public string Address
        {
            get { return il_Address; }
            set { il_Address = value; OnPropertyChanged("Address"); }
        }

        public string ContactNumbers
        {
            get { return il_ContactNumbers; }
            set { il_ContactNumbers = value; OnPropertyChanged("ContactNumbers");
            }
        }

        public string ContactPerson
        {
            get { return il_ContactPerson; }
            set { il_ContactPerson = value; OnPropertyChanged("ContactPerson"); }
        }

        public bool IsActive
        {
            get { return il_IsActive; }
            set { il_IsActive = value; OnPropertyChanged("IsActive"); }
        }

        public bool IsSystem
        {
            get { return il_IsSystem; }
            set { il_IsSystem = value; OnPropertyChanged("IsSystem"); }
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
        private static readonly string[] PropertiesToValidate = { "CompanyName" };

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
            if (columnName == "CompanyName" && this.CompanyName == "")
                result = "Name can not be empty";

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
