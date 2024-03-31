using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class DiscountDetail : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long il_DiscountId;
        private decimal? il_Amount;
        private decimal? il_Percentage;
        private bool il_IsActive;
        private long il_CreatedByUserId;
        private DateTime il_CreatedDate;
        private long il_UpdatedByUserId;
        private DateTime il_UpdatedDate;
        private bool il_IsAmount;
        private bool il_IsPercentage;

        [Key]
        public long Id
        {
            get { return il_Id; }
            set { il_Id = value; OnPropertyChanged("Id"); }
        }

        public long DiscountId
        {
            get { return il_DiscountId; }
            set { il_DiscountId = value; OnPropertyChanged("DiscountId"); }
        }

        public decimal? Amount
        {
            get { return il_Amount; }
            set { 
                il_Amount = value;
                OnPropertyChanged("Amount");
                DiscountDetailAmount = String.Format("{0:N}", value);
                IsAmount = value != null;
            }
        }

        public decimal? Percentage
        {
            get { return il_Percentage; }
            set {
                il_Percentage = value;
                OnPropertyChanged("Percentage");
                DiscountDetailPercentage = String.Format("{0:N}", value);
                IsPercentage = value != null;
            }
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
        public bool IsAmount
        {
            get { return il_IsAmount; }
            set {
                il_IsAmount = value;
                if (value)
                    Percentage = null;

                OnPropertyChanged("IsAmount");
            }
        }

        [NotMapped]
        public bool IsPercentage
        {
            get { return il_IsPercentage; }
            set
            {
                il_IsPercentage = value;
                if (value)
                    Amount = null;

                OnPropertyChanged("IsPercentage");
            }
        }

        [NotMapped]
        public string DiscountDetailAmount { get; set; }

        [NotMapped]
        public string DiscountDetailPercentage { get; set; }

        [NotMapped]
        public string GroupName { get; set; }

        #region Validation
        private static readonly string[] _propertiesToValidate = { "DiscountDetailAmount", "DiscountDetailPercentage" };

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

            if (columnName == "DiscountDetailAmount")
            {
                if (this.DiscountDetailAmount != null && this.DiscountDetailAmount.Trim() != string.Empty)
                {
                    decimal discountAmount = 0;
                    bool isDecimal = decimal.TryParse(this.DiscountDetailAmount, out discountAmount);
                    if (!isDecimal)
                        result = $"\r\nDiscount Detail Amount of {this.DiscountDetailAmount} is invalid.";
                }
            }
            if (columnName == "DiscountDetailPercentage")
            {
                if (this.DiscountDetailPercentage != null && this.DiscountDetailPercentage.Trim() != string.Empty)
                {
                    decimal discountPercentage = 0;
                    bool isDecimal = decimal.TryParse(this.DiscountDetailPercentage, out discountPercentage);
                    if (!isDecimal)
                        result = $"\r\nDiscount Detail Percentage of {this.DiscountDetailPercentage} is invalid.";
                }
            }

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
