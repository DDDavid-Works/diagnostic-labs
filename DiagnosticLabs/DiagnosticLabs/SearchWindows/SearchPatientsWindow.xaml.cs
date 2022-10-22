using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models.Views;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchPatientsWindow.xaml
    /// </summary>
    public partial class SearchPatientsWindow : Window
    {
        public PatientCompany SelectedPatientCompany { get; set; }

        public SearchPatientsWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchPatientViewModel();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatients();
        }

        private void PatientsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectPatientCompany();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectPatientCompany();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                PatientsDataGrid.Focus();
        }
        #endregion

        #region Data to/from UI
        private void SearchPatients()
        {
            SearchPatientViewModel vm = (SearchPatientViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }

        private void SelectPatientCompany()
        {
            if (PatientsDataGrid.Items.Count == 0) return;

            SelectedPatientCompany = (PatientCompany)PatientsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
