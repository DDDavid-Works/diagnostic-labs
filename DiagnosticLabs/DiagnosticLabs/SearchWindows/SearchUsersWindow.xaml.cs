using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchUserWindow.xaml
    /// </summary>
    public partial class SearchUsersWindow : Window
    {
        UsersBLL usersBLL = new UsersBLL();

        public User SelectedUser { get; set; }

        public SearchUsersWindow()
        {
            InitializeComponent();
            LoadUserList();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadUserList(NameFilterTextBox.Text);
        }

        private void UsersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectUser();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectUser();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void LoadUserList(string name = null)
        {
            UsersDataGrid.ItemsSource = UserList(name);
            UsersDataGrid.Items.Refresh();
        }

        private List<User> UserList(string name = null)
        {
            if (name == null)
                return usersBLL.GetAllUsers();
            else
                return usersBLL.GetUsers(name);
        }

        private void SelectUser()
        {
            if (UsersDataGrid.Items.Count == 0) return;

            SelectedUser = (User)UsersDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
