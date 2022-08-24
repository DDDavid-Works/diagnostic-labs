using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;

namespace DiagnosticLabs.ManagementWindows
{
    /// <summary>
    /// Interaction logic for DepartmentsWindow.xaml
    /// </summary>
    public partial class DepartmentsWindow : Window
    {
        public Department SelectedDepartment { get; set; }

        public DepartmentsWindow()
        {
            InitializeComponent();
            this.DataContext = new DepartmentViewModel(0);
        }

        #region UI Events
        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchDepartmentsWindow search = new SearchDepartmentsWindow();
            search.ShowDialog();

            if (search.SelectedDepartment == null) return;

            this.DataContext = new DepartmentViewModel(search.SelectedDepartment.Id);
        }
        #endregion

        #region Data to/from UI
        #endregion
    }
}
