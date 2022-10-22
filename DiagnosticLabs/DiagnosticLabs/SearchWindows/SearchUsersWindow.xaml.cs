using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchUsersWindow.xaml
    /// </summary>
    public partial class SearchUsersWindow : Window
    {
        public User SelectedUser { get; set; }

        public SearchUsersWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchUserViewModel();
            NameFilterTextBox.Focus();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchUsers();
        }

        private void UsersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectUsers();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectUsers();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                UsersDataGrid.Focus();
        }
        #endregion

        #region Data to/from UI
        private void SearchUsers()
        {
            SearchUserViewModel vm = (SearchUserViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }

        private void SelectUsers()
        {
            if (UsersDataGrid.Items.Count == 0) return;

            SelectedUser = (User)UsersDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
