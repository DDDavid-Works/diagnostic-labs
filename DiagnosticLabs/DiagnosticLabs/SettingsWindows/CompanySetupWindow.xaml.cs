using DiagnosticLabs.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DiagnosticLabs.SettingsWindows
{
    /// <summary>
    /// Interaction logic for CompanySetupWindow.xaml
    /// </summary>
    public partial class CompanySetupWindow : Window
    {
        public CompanySetupWindow()
        {
            InitializeComponent();
            this.DataContext = new CompanySetupViewModel();
        }

        private void EditLogoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var vm = (CompanySetupViewModel)DataContext;
                if (vm.ChangeLogoCommand.CanExecute(null))
                {
                    ImageSource imageSource = new BitmapImage(new Uri(dlg.FileName));
                    vm.ChangeLogoCommand.Execute(imageSource);
                }
            }
        }
    }
}
