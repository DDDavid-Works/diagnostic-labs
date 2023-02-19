using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private const string _entityName = "ChangePassword";

        CommonFunctions _commonFunctions = new CommonFunctions();
        UsersBLL _usersBLL = new UsersBLL();

        #region Public Properties
        public User User { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand UpdateOldPasswordCommand { get; set; }
        public ICommand UpdateNewPasswordCommand { get; set; }
        public ICommand UpdateConfirmPasswordCommand { get; set; }

        #endregion

        public ChangePasswordViewModel(long userId)
        {
            if (userId != 0)
            {
                this.User = _usersBLL.GetUser(userId);
                this.User.OriginalPassword = this.User.Password.Clone().ToString();
                this.User.OldPassword = string.Empty;
            }
            else
                this.User = new User() { Id = 0, Username = string.Empty, Password = string.Empty };

            this.SaveCommand = new RelayCommand(param => SavePassword());
            this.UpdateOldPasswordCommand = new RelayCommand(param => UpdateOldPassword((string)param));
            this.UpdateNewPasswordCommand = new RelayCommand(param => UpdateNewPassword((string)param));
            this.UpdateConfirmPasswordCommand = new RelayCommand(param => UpdateConfirmPassword((string)param));
        }

        #region Data Actions
        private void SavePassword()
        {
            if (!this.User.IsValid || !this.IsValid())
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.User.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.User.Id;
            this.User.Password = _commonFunctions.HashPassword(this.User.NewPassword);
            if (_usersBLL.SaveUser(this.User, ref id))
            {
                this.User.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }
        #endregion

        #region Private Methods
        private void UpdateOldPassword(string password)
        {
            this.User.OldPassword = password;
        }

        private void UpdateNewPassword(string password)
        {
            this.User.NewPassword = password;
        }

        private void UpdateConfirmPassword(string confirmPassword)
        {
            this.User.ConfirmPassword = confirmPassword;
        }
        #endregion

        #region Validation
        private bool IsValid()
        {
            string result = string.Empty;

            if (this.User.Id == 0)
                result = "Please select a user first.";
            else
            {
                if (this.User.NewPassword != this.User.ConfirmPassword)
                    result = "\r\nPassword does not match.";

                if (this.User.OriginalPassword != string.Empty && this.User.OldPassword != string.Empty)
                    if (this.User.OriginalPassword != _commonFunctions.HashPassword(this.User.OldPassword))
                        result += "\r\nOld password is incorrect.";
            }

            this.User.ErrorMessages += result;
            this.User.ErrorMessages = this.User.ErrorMessages.Trim('\r', '\n');

            return result.Trim() == string.Empty;
        }
        #endregion
    }
}
