using DiagnosticLabsDAL.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models
{
    public class ModuleType : BaseModel, IDataErrorInfo
    {
        private int il_Id;
        private string il_ModuleTypeName;
        private string il_Icon;
        private int il_SortOrder;
        private bool il_IsActive;
        private bool il_IsAdmin;

        [Key]
        public int Id
        {
            get { return il_Id; }
            set { il_Id = value; OnPropertyChanged("Id"); }
        }

        public string ModuleTypeName
        {
            get { return il_ModuleTypeName; }
            set { il_ModuleTypeName = value; OnPropertyChanged("ModuleTypeName"); }
        }

        public string Icon
        {
            get { return il_Icon; }
            set { il_Icon = value; OnPropertyChanged("Icon"); }
        }

        public int SortOrder
        {
            get { return il_SortOrder; }
            set { il_SortOrder = value; OnPropertyChanged("SortOrder"); }
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

        #region Validation
        private static readonly string[] _propertiesToValidate = { };

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
