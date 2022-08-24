using DiagnosticLabsDAL.Models.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class UserPermission : BaseModel, IDataErrorInfo, ICloneable
    {
        private long il_Id;
        private long il_UserId;
        private int il_ModuleId;
        private bool il_ViewOnly;
        private bool il_AllowCreate;
        private bool il_AllowEdit;
        private bool il_AllowDelete;
        private bool il_AllowPrint;
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

        public long UserId
        {
            get { return il_UserId; }
            set { il_UserId = value; OnPropertyChanged("UserId"); }
        }

        public int ModuleId
        {
            get { return il_ModuleId; }
            set { il_ModuleId = value; OnPropertyChanged("ModuleId"); }
        }

        public bool ViewOnly
        {
            get { return il_ViewOnly; }
            set { il_ViewOnly = value; OnPropertyChanged("ViewOnly"); }
        }

        public bool AllowCreate
        {
            get { return il_AllowCreate; }
            set { il_AllowCreate = value; OnPropertyChanged("AllowCreate"); }
        }

        public bool AllowEdit
        {
            get { return il_AllowEdit; }
            set { il_AllowEdit = value; OnPropertyChanged("AllowEdit"); }
        }

        public bool AllowDelete
        {
            get { return il_AllowDelete; }
            set { il_AllowDelete = value; OnPropertyChanged("AllowDelete"); }
        }

        public bool AllowPrint
        {
            get { return il_AllowPrint; }
            set { il_AllowPrint = value; OnPropertyChanged("AllowPrint"); }
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

            return result;
        }

        public object Clone()
        {
            return new UserPermission()
            {
                Id = this.Id,
                UserId = this.UserId,
                ModuleId = this.ModuleId,
                ViewOnly = this.ViewOnly,
                AllowCreate = this.AllowCreate,
                AllowEdit = this.AllowEdit,
                AllowDelete = this.AllowDelete,
                AllowPrint = this.AllowPrint,
                CreatedByUserId = this.CreatedByUserId,
                CreatedDate = this.CreatedDate,
                UpdatedByUserId = this.UpdatedByUserId,
                UpdatedDate = this.UpdatedDate
            };
        }
        #endregion
    }
}
