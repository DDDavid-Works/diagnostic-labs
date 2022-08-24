using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchDepartmentsWindow.xaml
    /// </summary>
    public partial class SearchDepartmentsWindow : Window
    {
        DepartmentsBLL departmentsBLL = new DepartmentsBLL();

        public Department SelectedDepartment { get; set; }

        public SearchDepartmentsWindow()
        {
            InitializeComponent();
            LoadDepartmentList();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDepartmentList(NameFilterTextBox.Text);
        }

        private void DepartmentsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectDepartment();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectDepartment();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void LoadDepartmentList(string name = null)
        {
            DepartmentsDataGrid.ItemsSource = DepartmentList(name);
            DepartmentsDataGrid.Items.Refresh();
        }

        private List<Department> DepartmentList(string name = null)
        {
            if (name == null)
                return departmentsBLL.GetAllDepartments();
            else
                return departmentsBLL.GetDepartments(name);
        }

        private void SelectDepartment()
        {
            if (DepartmentsDataGrid.Items.Count == 0) return;

            SelectedDepartment = (Department)DepartmentsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
