using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class Discount : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private string il_DiscountName;
        private string il_DiscountDescription;
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

        public string DiscountName
        {
            get { return il_DiscountName; }
            set { il_DiscountName = value; OnPropertyChanged("DiscountName"); }
        }

        public string DiscountDescription
        {
            get { return il_DiscountDescription; }
            set { il_DiscountDescription = value; OnPropertyChanged("DiscountDescription"); }
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
            set { il_CreatedDate = value; OnPropertyChanged("CreatedDate");}
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
        private static readonly string[] _propertiesToValidate = { "DiscountName", "DiscountDescription", "DiscountPrice" };

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
                foreach (string property in _propertiesToValidate)
                    if (GetValidationError(property) != string.Empty)
                        errorFound = true;

                return !errorFound;
            }
        }

        private string GetValidationError(string columnName)
        {
            string result = string.Empty;
            if (columnName == "DiscountName" && this.DiscountName.Trim() == string.Empty)
                result = "Name can not be empty.";
            else if (columnName == "DiscountDescription" && this.DiscountDescription.Trim() == string.Empty)
                result = "\r\nDescription can not be empty.";

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
