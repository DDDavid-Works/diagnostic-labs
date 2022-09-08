using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class PatientRegistration : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private DateTime il_InputDate;
        private long? il_PatientId;
        private long? il_CompanyId;
        private long? il_PackageId;
        private string il_BatchName;
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

        public DateTime InputDate
        {
            get { return il_InputDate; }
            set { il_InputDate = value; OnPropertyChanged("InputDate"); }
        }

        public long? PatientId
        {
            get { return il_PatientId; }
            set { il_PatientId = value; OnPropertyChanged("PatientId"); }
        }

        public long? CompanyId
        {
            get { return il_CompanyId; }
            set { il_CompanyId = value; OnPropertyChanged("CompanyId"); }
        }

        public long? PackageId
        {
            get { return il_PackageId; }
            set { il_PackageId = value; OnPropertyChanged("PackageId"); }
        }

        public string BatchName
        {
            get { return il_BatchName; }
            set { il_BatchName = value; OnPropertyChanged("BatchName"); }
        }

        public decimal Price
        {
            get { return il_Price; }
            set
            {
                il_Price = value;
                PatientRegistrationPrice = String.Format("{0:0,0.00}", value);
                OnPropertyChanged("Price");
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
        public string PatientRegistrationPrice { get; set; }

        [NotMapped]
        public bool IsPriceEdited { get; set; }

        #region Validation
        private static readonly string[] PropertiesToValidate = { "PatientRegistrationPrice" };

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

            if (columnName == "PatientRegistrationPrice")
            {
                decimal patientRegistrationPrice = 0;
                bool isDecimal = decimal.TryParse(this.PatientRegistrationPrice, out patientRegistrationPrice);
                if (!isDecimal)
                    result = "\r\nPrice is invalid.";
            }

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
