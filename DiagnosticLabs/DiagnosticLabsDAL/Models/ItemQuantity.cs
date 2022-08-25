using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class ItemQuantity : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long il_ItemId;
        private long il_ItemLocationId;
        private decimal il_Quantity;
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

        public long ItemId
        {
            get { return il_ItemId; }
            set { il_ItemId = value; OnPropertyChanged("ItemId"); }
        }

        public long ItemLocationId
        {
            get { return il_ItemLocationId; }
            set { il_ItemLocationId = value; OnPropertyChanged("ItemLocationId"); }
        }

        public decimal Quantity
        {
            get { return il_Quantity; }
            set { il_Quantity = value; OnPropertyChanged("Quantity"); }
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
        private static readonly string[] PropertiesToValidate = { };

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
            //if (columnName == "Username" && this.Username.Trim() == string.Empty)
            //    result = "User Name can not be empty.";
            //else if (columnName == "Password" && this.Password.Trim() == string.Empty)
            //    result = "\r\nPassword can not be empty.";

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
