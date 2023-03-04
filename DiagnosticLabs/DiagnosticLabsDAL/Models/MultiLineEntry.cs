using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class MultiLineEntry : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private int? il_ModuleId;
        private string il_FieldName;
        private string il_FieldValueTitle;
        private string il_FieldValue;
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

        public int? ModuleId
        {
            get { return il_ModuleId; }
            set { il_ModuleId = value; OnPropertyChanged("ModuleId"); }
        }

        public string FieldName
        {
            get { return il_FieldName; }
            set { il_FieldName = value; OnPropertyChanged("FieldName"); }
        }

        public string FieldValueTitle
        {
            get { return il_FieldValueTitle; }
            set { il_FieldValueTitle = value; OnPropertyChanged("FieldValueTitle"); }
        }

        public string FieldValue
        {
            get { return il_FieldValue; }
            set { il_FieldValue = value; OnPropertyChanged("FieldValue"); }
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
        public bool IsSelected { get; set; } = false;

        [NotMapped]
        public bool IsDefault { get; set; }

        #region Validation
        private static readonly string[] _propertiesToValidate = { "FieldValueTitle" ,"FieldValue" };

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
            if (columnName == "FieldValueTitle" && this.FieldValueTitle.Trim() == string.Empty)
                result = "Entry Name can not be empty.";
            else if (columnName == "FieldValue" && this.FieldValue.Trim() == string.Empty)
                result = "Entry Value can not be empty.";

            ErrorMessages += result;
            ErrorMessages = ErrorMessages.Trim('\r', '\n');

            return result;
        }
        #endregion
    }
}
