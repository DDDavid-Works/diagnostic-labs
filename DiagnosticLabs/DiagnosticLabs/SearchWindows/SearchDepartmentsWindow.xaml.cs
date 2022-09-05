using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchDepartmentsWindow.xaml
    /// </summary>
    public partial class SearchDepartmentsWindow : Window
    {
        public Department SelectedDepartment { get; set; }

        public SearchDepartmentsWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchDepartmentViewModel();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchDepartmentViewModel vm = (SearchDepartmentViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(null);
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
        private void SelectDepartment()
        {
            if (DepartmentsDataGrid.Items.Count == 0) return;

            SelectedDepartment = (Department)DepartmentsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
