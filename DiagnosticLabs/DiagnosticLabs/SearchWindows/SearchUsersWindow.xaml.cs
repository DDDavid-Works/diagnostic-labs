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
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchUserViewModel vm = (SearchUserViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(null);
        }

        private void UsersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectUserLocation();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectUserLocation();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void SelectUserLocation()
        {
            if (UsersDataGrid.Items.Count == 0) return;

            SelectedUser = (User)UsersDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
