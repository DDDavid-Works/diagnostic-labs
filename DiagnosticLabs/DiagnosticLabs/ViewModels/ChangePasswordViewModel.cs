using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private const string EntityName = "ChangePassword";

        UsersBLL usersBLL = new UsersBLL();

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
                this.User = usersBLL.GetUser(userId);
                this.User.OriginalPassword = this.User.Password.Clone().ToString();
                this.User.OldPassword = string.Empty;
            }
            else
                this.User = new User() { Id = 0 };

            this.User.IsChangePassword = true;

            this.SaveCommand = new RelayCommand(param => SavePassword());
            this.UpdateOldPasswordCommand = new RelayCommand(param => UpdateOldPassword((string)param));
            this.UpdateNewPasswordCommand = new RelayCommand(param => UpdateNewPassword((string)param));
            this.UpdateConfirmPasswordCommand = new RelayCommand(param => UpdateConfirmPassword((string)param));
        }

        #region Data Actions
        private void SavePassword()
        {
            if (!this.User.IsValid)
            {
                string errorMessages = this.User.ErrorMessages;
                MessageBox.Show(errorMessages, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            long id = this.User.Id;
            if (usersBLL.SaveUser(this.User, ref id))
            {
                this.User.Id = id;
                MessageBox.Show(Messages.SavedSuccessfully, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(Messages.SaveFailed, EntityName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region Private Methods
        private void UpdateOldPassword(string password)
        {
            this.User.OldPassword = password;
        }

        private void UpdateNewPassword(string password)
        {
            this.User.Password = password;
        }

        private void UpdateConfirmPassword(string confirmPassword)
        {
            this.User.ConfirmPassword = confirmPassword;
        }
        #endregion
    }
}
