using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class Service : BaseModel, IDataErrorInfo 
    {
        private long il_Id;
        private string il_ServiceName;
        private string il_ServiceDescription;
        private decimal il_Price;
        private bool il_IsActive;
        private long il_CreatedByUserId;
        private DateTime il_CreatedDate;
        private long il_UpdatedByUserId;
        private DateTime il_UpdatedDate;

        [Key]
        public long Id
        {
            get
            {
                return il_Id;
            }
            set
            {
                il_Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string ServiceName
        {
            get
            {
                return il_ServiceName;
            }
            set
            {
                il_ServiceName = value;
                OnPropertyChanged("ServiceName");
            }
        }

        public string ServiceDescription
        {
            get
            {
                return il_ServiceDescription;
            }
            set
            {
                il_ServiceDescription = value;
                OnPropertyChanged("ServiceDescription");
            }
        }

        public decimal Price
        {
            get
            {
                return il_Price;
            }
            set
            {
                il_Price = value;
                OnPropertyChanged("Price");
            }
        }

        public bool IsActive
        {
            get
            {
                return il_IsActive;
            }
            set
            {
                il_IsActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public long CreatedByUserId
        {
            get
            {
                return il_CreatedByUserId;
            }
            set
            {
                il_CreatedByUserId = value;
                OnPropertyChanged("CreatedByUserId");
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return il_CreatedDate;
            }
            set
            {
                il_CreatedDate = value;
                OnPropertyChanged("CreatedDate");
            }
        }

        public long UpdatedByUserId
        {
            get
            {
                return il_UpdatedByUserId;
            }
            set
            {
                il_UpdatedByUserId = value;
                OnPropertyChanged("UpdatedByUserId");
            }
        }

        public DateTime UpdatedDate
        {
            get
            {
                return il_UpdatedDate;
            }
            set
            {
                il_UpdatedDate = value;
                OnPropertyChanged("UpdatedDate");
            }
        }

        [NotMapped]
        public string ServicePrice { get; set; }
        
        #region Validation
        private static readonly string[] PropertiesToValidate = { "ServiceName", "ServiceDescription", "ServicePrice" };

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
            if (columnName == "ServiceName" && this.ServiceName.Trim() == string.Empty)
                result = "Name can not be empty.";
            else if (columnName == "ServiceDescription" && this.ServiceDescription.Trim() == string.Empty)
                result = "\r\nDescription can not be empty.";
            else if (columnName == "ServicePrice")
            {
                decimal servicePrice = 0;
                bool isDecimal = decimal.TryParse(this.ServicePrice, out servicePrice);
                if (!isDecimal)
                    result = "\r\nService Price is invalid.";
            }

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
