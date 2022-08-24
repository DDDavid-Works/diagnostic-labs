using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using System.Windows;

namespace DiagnosticLabs.SettingsWindows
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchUsersWindow search = new SearchUsersWindow();
            search.ShowDialog();

            if (search.SelectedUser == null) return;

            this.DataContext = new UserViewModel(search.SelectedUser.Id);
        }

        private void ViewOnlyCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var vm = (UserViewModel)DataContext;
            if (vm.UpdateUserPermissionCommand.CanExecute(null))
            {
                UserPermissionViewModel upvm = (UserPermissionViewModel)((System.Windows.Controls.CheckBox)sender).CommandParameter;
                vm.UpdateUserPermissionCommand.Execute(upvm);
            }
        }

        private void IsAdminCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var vm = (UserViewModel)DataContext;
            if (vm.UpdateUserPermissionCommand.CanExecute(null))
                vm.UpdateIsAdminCommand.Execute(null);
        }
    }
}
