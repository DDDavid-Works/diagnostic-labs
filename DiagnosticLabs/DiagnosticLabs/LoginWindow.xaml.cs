using DiagnosticLabs.ViewModels;
using System.Windows;

namespace DiagnosticLabs
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();

            UsernameTextBox.Focus();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                LogIn();
        }

        private void LogIn()
        {
            var vm = (LoginViewModel)DataContext;
            if (vm.LoginCommand.CanExecute(null))
            {
                vm.Username = UsernameTextBox.Text;
                vm.Password = PasswordPasswordBox.Password;
                vm.LoginCommand.Execute(null);

                if (vm.IsLoginSuccess)
                {
                    MainWindow mainWindow = new MainWindow(vm.LoggedUserId);
                    mainWindow.Show();
                    this.Close();
                }
            }
        }
    }
}
