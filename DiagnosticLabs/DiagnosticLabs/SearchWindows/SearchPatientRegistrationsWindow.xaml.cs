using DiagnosticLabs.ViewModels;
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
        public PatientRegistrationDetail SelectedPatientRegistrationDetail { get; set; }

        public SearchPatientRegistrationsWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchPatientRegistrationViewModel();
            NameFilterTextBox.Focus();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientRegistrationDetails();
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

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                PatientRegistrationDetailsDataGrid.Focus();
        }
        #endregion

        #region Data to/from UI
        private void SearchPatientRegistrationDetails()
        {
            SearchPatientRegistrationViewModel vm = (SearchPatientRegistrationViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }

        private void SelectPatientRegistrationDetail()
        {
            if (PatientRegistrationDetailsDataGrid.Items.Count == 0) return;

            SelectedPatientRegistrationDetail = (PatientRegistrationDetail)PatientRegistrationDetailsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
