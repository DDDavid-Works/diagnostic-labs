using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DiagnosticLabs.ViewModels
{
    public class CompanySetupViewModel : BaseViewModel
    {
        private const string _entityName = "CompanySetup";

        CommonFunctions _commonFunctions = new CommonFunctions();
        CompanySetupBLL _companySetupBLL = new CompanySetupBLL();

        #region Public Properties
        public CompanySetup CompanySetup { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ChangeLogoCommand { get; set; }

        private ImageSource _logoImageSource;
        public ImageSource LogoImageSource
        {
            get { return _logoImageSource; }
            set { _logoImageSource = value; OnPropertyChanged("LogoImageSource"); }
        }
        #endregion

        public CompanySetupViewModel()
        {
            this.CompanySetup = _companySetupBLL.GetLatestCompanySetup();
            this.SaveCommand = new RelayCommand(param => SaveCompanySetup());
            this.ChangeLogoCommand = new RelayCommand(param => ChangeLogo((ImageSource)param));
            LoadLogo();
        }

        #region Data Actions
        private void SaveCompanySetup()
        {
            if (!this.CompanySetup.IsValid)
            {
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.CompanySetup.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            if (_companySetupBLL.SaveCompanySetup(this.CompanySetup))
                this.NotificationMessage = Messages.SavedSuccessfully;
            else
                this.NotificationMessage = Messages.SaveFailed;
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

        private void ChangeLogo(ImageSource logoImageSource)
        {
            if (logoImageSource != null)
            {
                byte[] logoBytes = null;
                var bitmapSource = logoImageSource as BitmapSource;
                if (bitmapSource != null)
                {
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    using (var stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        logoBytes = stream.ToArray();
                    }
                }
                this.CompanySetup.Logo = logoBytes;
                this.LogoImageSource = logoImageSource;
            }
        }
        #endregion
    }
}
