using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class PatientRegistrationService : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private long il_PatientRegistrationId;
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

        public long PatientRegistrationId
        {
            get { return il_PatientRegistrationId; }
            set { il_PatientRegistrationId = value; OnPropertyChanged("PatientRegistrationId"); }
        }

        public long ServiceId
        {
            get { return il_ServiceId; }
            set { il_ServiceId = value; OnPropertyChanged("ServiceId"); }
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
        public string PatientRegistrationServiceName { get; set; }

        [NotMapped]
        public string PatientRegistrationServicePrice { get; set; }

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
                bool isDecimal = decimal.TryParse(this.PatientRegistrationServicePrice, out packageServicePrice);
                if (!isDecimal)
                    result = $"\r\nPackage Service Price of {this.PatientRegistrationServiceName} is invalid.";
            }

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
