using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using System.Windows;

namespace DiagnosticLabs.SettingsWindows
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
            this.DataContext = new ChangePasswordViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchUsersWindow search = new SearchUsersWindow();
            search.ShowDialog();

            if (search.SelectedUser == null) return;

            this.DataContext = new ChangePasswordViewModel(search.SelectedUser.Id);
        }

        private void OldPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = (ChangePasswordViewModel)DataContext;

            if (vm.UpdateOldPasswordCommand.CanExecute(null))
                vm.UpdateOldPasswordCommand.Execute(OldPasswordPasswordBox.Password);
        }


        private void NewPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = (ChangePasswordViewModel)DataContext;

            if (vm.UpdateNewPasswordCommand.CanExecute(null))
                vm.UpdateNewPasswordCommand.Execute(NewPasswordPasswordBox.Password);
        }

        private void ConfirmPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = (ChangePasswordViewModel)DataContext;

            if (vm.UpdateConfirmPasswordCommand.CanExecute(null))
                vm.UpdateConfirmPasswordCommand.Execute(ConfirmPasswordPasswordBox.Password);
        }
    }
}
