using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabs.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for PatientRegistrationSearchByCodeUserControl.xaml
    /// </summary>
    public partial class PatientRegistrationSearchByCodeUserControl : UserControl
    {
        public PatientRegistrationSearchByCodeUserControl()
        {
            InitializeComponent();
        }

        private void SearchPatientRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientRegistrationsWindow search = new SearchPatientRegistrationsWindow();
            search.ShowDialog();

            if (search.SelectedPatientRegistrationDetail == null) return;

            var vm = (BasePatientRegistrationViewModel)DataContext;
            if (vm.GetPatientRegistrationCommand.CanExecute(null))
            {
                long patientRegistrationId = search.SelectedPatientRegistrationDetail.PatientRegistrationId;
                vm.GetPatientRegistrationCommand.Execute(patientRegistrationId);
            }
        }

        private void RegistrationCodeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext.GetType().Equals(typeof(PaymentViewModel)))
            {
                var vm = (PaymentViewModel)DataContext;

                if (vm.GetPatientRegistrationByCodeCommand.CanExecute(null))
                    vm.GetPatientRegistrationByCodeCommand.Execute(((TextBox)sender).Text);
            }
            else if (DataContext.GetType().Equals(typeof(BaseLabResultsViewModel)))
            {
                var vm = (BaseLabResultsViewModel)DataContext;

                if (vm.GetPatientRegistrationByCodeCommand.CanExecute(null))
                    vm.GetPatientRegistrationByCodeCommand.Execute(((TextBox)sender).Text);
            }
        }
    }
}
