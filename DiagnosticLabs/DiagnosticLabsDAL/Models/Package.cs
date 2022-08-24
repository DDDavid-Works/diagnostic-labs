using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class Package : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private string il_PackageName;
        private string il_PackageDescription;
        private decimal il_Price;
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

        public string PackageName
        {
            get { return il_PackageName; }
            set { il_PackageName = value; OnPropertyChanged("PackageName"); }
        }

        public string PackageDescription
        {
            get { return il_PackageDescription; }
            set { il_PackageDescription = value; OnPropertyChanged("PackageDescription"); }
        }

        public decimal Price
        {
            get { return il_Price; }
            set {
                il_Price = value;
                PackagePrice = value.ToString();
                OnPropertyChanged("Price");
            }
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

        [NotMapped]
        public string PackagePrice { get; set; }

        #region Validation
        private static readonly string[] PropertiesToValidate = { "PackageName", "PackageDescription", "PackagePrice" };

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
            if (columnName == "PackageName" && this.PackageName.Trim() == string.Empty)
                result = "Name can not be empty.";
            else if (columnName == "PackageDescription" && this.PackageDescription.Trim() == string.Empty)
                result = "\r\nDescription can not be empty.";
            else if (columnName == "PackagePrice")
            {
                decimal packagePrice = 0;
                bool isDecimal = decimal.TryParse(this.PackagePrice, out packagePrice);
                if (!isDecimal)
                    result = "\r\nPackage Price is invalid.";
            }

            ErrorMessages += result;

            return result;
        }
        #endregion
    }
}
