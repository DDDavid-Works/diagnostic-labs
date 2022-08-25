using DiagnosticLabsDAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiagnosticLabsDAL.Models
{
    public class CompanySetup : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private string il_CompanyName;
        private string il_SubCompanyName;
        private string il_Tagline;
        private string il_Address;
        private string il_ContactNumbers;
        private string il_Email;
        private byte[] il_Logo;
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

        public string SubCompanyName
        {
            get { return il_SubCompanyName; }
            set { il_SubCompanyName = value; OnPropertyChanged("SubCompanyName"); }
        }

        public string Tagline
        {
            get { return il_Tagline; }
            set { il_Tagline = value; OnPropertyChanged("Tagline"); }
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

        public string Email
        {
            get { return il_Email; }
            set { il_Email = value; OnPropertyChanged("Email"); }
        }

        public byte[] Logo
        {
            get { return il_Logo; }
            set { il_Logo = value; OnPropertyChanged("Logo"); }
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
        private static readonly string[] PropertiesToValidate = { "CompanyName", "Address", "ContactNumber" };

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
            if (columnName == "CompanyName" && this.CompanyName.Trim() == string.Empty)
                result = "Name can not be empty.";
            else if (columnName == "Address" && this.Address.Trim() == string.Empty)
                result = "\r\nAddress can not be empty.";
            else if (columnName == "ContactNumber" && this.Address.Trim() == string.Empty)
                result = "\r\nContact Number can not be empty.";

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
