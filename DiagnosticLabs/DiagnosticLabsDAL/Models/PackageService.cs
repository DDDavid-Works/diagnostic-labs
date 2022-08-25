using DiagnosticLabsDAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiagnosticLabsDAL.Models
{
    public class PackageService : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long il_PackageId;
        private long il_ServiceId;
        private decimal il_Price;
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

        public long PackageId
        {
            get { return il_PackageId; }
            set { il_PackageId = value; OnPropertyChanged("PackageId"); }
        }

        public long ServiceId
        {
            get { return il_ServiceId; }
            set { il_ServiceId = value; OnPropertyChanged("ServiceId");}
        }

        public decimal Price
        {
            get { return il_Price; }
            set { il_Price = value; OnPropertyChanged("Price"); }
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
        public string PackageServiceName { get; set; }

        [NotMapped]
        public string PackageServicePrice { get; set; }

        #region Validation
        private static readonly string[] PropertiesToValidate = { "PackageServicePrice" };

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
            if (columnName == "PackageServicePrice")
            {
                decimal packageServicePrice = 0;
                bool isDecimal = decimal.TryParse(this.PackageServicePrice, out packageServicePrice);
                if (!isDecimal)
                    result = $"\r\nPackage Service Price of {this.PackageServiceName} is invalid.";
            }

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
