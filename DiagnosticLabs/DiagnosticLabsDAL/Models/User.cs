using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models
{
    public class User : BaseModel, IDataErrorInfo
    {
        private long il_Id;
        private string il_Username;
        private string il_FullName;
        private string il_Password;
        private bool il_IsActive;
        private bool il_IsAdmin;
        private long il_CreatedByUserId;
        private DateTime il_CreatedDate;
        private long il_UpdatedByUserId;
        private DateTime il_UpdatedDate;

        [Key]
        public long Id
        {
            get { return il_Id; }
            set { il_Id = value; OnPropertyChanged("Id");}
        }

        public string Username
        {
            get { return il_Username; }
            set { il_Username = value; OnPropertyChanged("Username"); }
        }

        public string FullName
        {
            get { return il_FullName; }
            set { il_FullName = value; OnPropertyChanged("FullName"); }
        }

        public string Password
        {
            get { return il_Password; }
            set { il_Password = value; OnPropertyChanged("Password"); }
        }

        public bool IsActive
        {
            get { return il_IsActive; }
            set { il_IsActive = value; OnPropertyChanged("IsActive"); }
        }

        public bool IsAdmin
        {
            get { return il_IsAdmin; }
            set { il_IsAdmin = value; OnPropertyChanged("IsAdmin"); }
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
        public bool IsChangePassword { get; set; } = false;

        [NotMapped]
        public string OriginalPassword { get; set; }

        [NotMapped]
        public string OldPassword { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }


        #region Validation
        private static readonly string[] PropertiesToValidate = { "Username" };

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

                if (!IsChangePassword)
                {
                    bool errorFound = false;
                    foreach (string property in PropertiesToValidate)
                        if (GetValidationError(property) != string.Empty)
                            errorFound = true;

                    return !errorFound;
                }
                else
                {
                    return (GetValidationError("ChangePassword") == string.Empty);
                }
            }
        }

        private string GetValidationError(string columnName)
        {
            string result = string.Empty;
            if (columnName == "Username" && this.Username.Trim() == string.Empty)
                result = "User Name can not be empty.";
            else if (columnName == "ChangePassword")
            {
                if (this.Id == 0)
                    result = "Please select a user first.";

                if (this.Password != this.ConfirmPassword)
                    result = "\r\nPassword does not match.";

                if (this.OriginalPassword != this.OldPassword)
                    result += "\r\nOld password is incorrect.";
            }

            ErrorMessages += result;

            return result;
        }
        #endregion
    }
}
