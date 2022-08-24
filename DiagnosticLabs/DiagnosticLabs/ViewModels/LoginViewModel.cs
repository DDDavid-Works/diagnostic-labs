using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DiagnosticLabs.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private const string EntityName = "Login";

        UsersBLL usersBLL = new UsersBLL();
        CompanySetupBLL companySetupBLL = new CompanySetupBLL();

        #region Public Properties
        public CompanySetup CompanySetup { get; set; }
        public ImageSource CompanyLogoImageSource { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLoginSuccess { get; set; } = false;
        public long LoggedUserId { get; set; } = 0;
        public string LoginErrorMessage { get; set; }
        public ICommand LoginCommand { get; set; }

        private ImageSource _logoImageSource;
        public ImageSource LogoImageSource
        {
            get { return _logoImageSource; }
            set { _logoImageSource = value; OnPropertyChanged("LogoImageSource"); }
        }
        #endregion

        public LoginViewModel()
        {
            this.CompanySetup = companySetupBLL.GetLatestCompanySetup();
            LoadLogo();

            this.Username = string.Empty;
            this.Password = string.Empty;
            this.LoginErrorMessage = string.Empty;
            this.LoginCommand = new RelayCommand(param => Login());
        }

        #region Data Actions
        private void Login()
        {
            //long userId = 0;
            //this.IsLoginSuccess = usersBLL.IsLoginSuccess(this.Username, this.Password, ref userId);
            //this.LoggedUserId = userId;

            //AUTO LOGIN
            this.IsLoginSuccess = true;
            this.LoggedUserId = 1;

            if (!this.IsLoginSuccess)
                this.LoginErrorMessage = "Login failed. Please try again.";
        }
        #endregion

        #region Private Methods
        private void LoadLogo()
        {
            if (this.CompanySetup.Logo != null)
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(this.CompanySetup.Logo);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                ImageSource logoImageSource = biImg as ImageSource;
                this.LogoImageSource = logoImageSource;
            }
        }
        #endregion
    }
}
