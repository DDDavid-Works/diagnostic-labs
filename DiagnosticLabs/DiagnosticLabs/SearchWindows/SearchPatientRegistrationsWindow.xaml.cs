using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models.Views;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchPatientRegistrationsWindow.xaml
    /// </summary>
    public partial class SearchPatientRegistrationsWindow : Window
    {
        PatientsBLL patientsBLL = new PatientsBLL();
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();

        public PatientRegistrationDetail SelectedPatientRegistrationDetail { get; set; }

        public SearchPatientRegistrationsWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchPatientRegistrationViewModel();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientRegistrationViewModel vm = (SearchPatientRegistrationViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(null);
        }

        private void PatientRegistrationDetailsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectPatientRegistrationDetail();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectPatientRegistrationDetail();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Data to/from UI
        private void SelectPatientRegistrationDetail()
        {
            if (PatientRegistrationDetailsDataGrid.Items.Count == 0) return;

            SelectedPatientRegistrationDetail = (PatientRegistrationDetail)PatientRegistrationDetailsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
