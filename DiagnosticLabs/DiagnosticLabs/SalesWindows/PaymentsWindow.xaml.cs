using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.SalesWindows
{
    /// <summary>
    /// Interaction logic for PaymentsWindow.xaml
    /// </summary>
    public partial class PaymentsWindow : Window
    {
        public PaymentsWindow()
        {
            InitializeComponent();
            this.DataContext = new PaymentViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchPaymentsWindow search = new SearchPaymentsWindow();
            search.ShowDialog();

            if (search.SelectedPaymentDetail == null) return;

            this.DataContext = new PaymentViewModel(search.SelectedPaymentDetail.PaymentId);
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            SearchServicesWindow search = new SearchServicesWindow();
            search.ShowDialog();

            if (search.SelectedService == null) return;

            var vm = (PaymentViewModel)DataContext;
            if (vm.AddPatientRegistrationServiceCommand.CanExecute(null))
            {
                PatientRegistrationServiceViewModel prsvm = new PatientRegistrationServiceViewModel()
                {
                    PatientRegistrationService = new PatientRegistrationService()
                    {
                        PatientRegistrationId = vm.PatientRegistration.Id,
                        ServiceId = search.SelectedService.Id,
                        Price = search.SelectedService.Price,
                        IsActive = true
                    },
                    Service = search.SelectedService
                };
                vm.AddPatientRegistrationServiceCommand.Execute(prsvm);
            }
        }

        private void EditServiceButton_Click(object sender, RoutedEventArgs e)
        {
            SearchServicesWindow search = new SearchServicesWindow();
            search.ShowDialog();

            if (search.SelectedService == null) return;

            var vm = (PatientRegistrationViewModel)DataContext;
            if (vm.UpdatePatientRegistrationServiceCommand.CanExecute(null))
            {
                PatientRegistrationServiceViewModel prsvm = (PatientRegistrationServiceViewModel)((Button)sender).CommandParameter;
                prsvm.PatientRegistrationService = new PatientRegistrationService()
                {
                    PatientRegistrationId = vm.PatientRegistration.Id,
                    ServiceId = search.SelectedService.Id,
                    Price = search.SelectedService.Price,
                    IsActive = true
                };
                prsvm.Service = search.SelectedService;
                vm.UpdatePatientRegistrationServiceCommand.Execute(prsvm);
            }
        }

        private void SearchPatientRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientRegistrationsWindow search = new SearchPatientRegistrationsWindow();
            search.ShowDialog();

            if (search.SelectedPatientRegistrationDetail == null) return;

            var vm = (PaymentViewModel)DataContext;
            if (vm.GetPatientRegistrationCommand.CanExecute(null))
            {
                long patientRegistrationId = search.SelectedPatientRegistrationDetail.PatientRegistrationId;
                vm.GetPatientRegistrationCommand.Execute(patientRegistrationId);
            }
        }

        private void PatientRegistrationPriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var vm = (PaymentViewModel)DataContext;

            if (vm.UpdateIsPriceEditedAndAmountDueCommand.CanExecute(null))
                vm.UpdateIsPriceEditedAndAmountDueCommand.Execute(null);
        }

        private void CashTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = (PaymentViewModel)DataContext;

            if (vm.ComputeTotalsCommand.CanExecute(null))
                vm.ComputeTotalsCommand.Execute(((TextBox)sender).Text);
        }
    }
}
