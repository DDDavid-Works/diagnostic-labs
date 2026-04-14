using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Linq;
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
            var vm = (PaymentViewModel)DataContext;

            SelectServicesWindow select = new SelectServicesWindow(vm.PatientRegistrationServices.Select(s => s.Service).ToList());

            select.ShowDialog();

            if (select.SelectedServices == null) return;

            if (vm.UpdateAllPatientRegistrationServicesCommand.CanExecute(null))
            {
                List<PatientRegistrationServiceViewModel> list = new List<PatientRegistrationServiceViewModel>();
                foreach (Service service in select.SelectedServices)
                {
                    PatientRegistrationServiceViewModel prsvm = new PatientRegistrationServiceViewModel()
                    {
                        PatientRegistrationService = new PatientRegistrationService()
                        {
                            PatientRegistrationId = vm.PatientRegistration.Id,
                            ServiceId = service.Id,
                            Price = service.Price,
                            IsActive = true
                        },
                        Service = service
                    };
                    list.Add(prsvm);
                }
                vm.UpdateAllPatientRegistrationServicesCommand.Execute(list);
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

        private void PatientRegistrationAmountDueTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var vm = (PaymentViewModel)DataContext;

            if (vm.UpdateIsPriceEditedAndAmountDueCommand.CanExecute(null))
            {
                decimal textBoxValue = 0;
                bool isDecimal = decimal.TryParse(((TextBox)sender).Text, out textBoxValue);
                if (isDecimal)
                {
                    bool isValueChanged = vm.PatientRegistration.AmountDue != textBoxValue;
                    vm.UpdateIsPriceEditedAndAmountDueCommand.Execute(isValueChanged);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (PaymentViewModel)DataContext;

            if (vm.GetPatientRegistrationCommand.CanExecute(null))
                vm.GetPatientRegistrationCommand.Execute(Globals.PATIENTREGISTRATIONIDTOPAY);
        }

        private void PaymentAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = (PaymentViewModel)DataContext;

            if (vm.ComputeTotalsCommand.CanExecute(null))
                vm.ComputeTotalsCommand.Execute(((TextBox)sender).Text);
        }

        private void DiscountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = (PaymentViewModel)DataContext;

            if (vm.ComputeDiscountsCommand.CanExecute(null))
                vm.ComputeDiscountsCommand.Execute(null);
        }

        private void DiscountRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PaymentViewModel)DataContext;

            if (vm.ComputeDiscountsCommand.CanExecute(null))
                vm.ComputeDiscountsCommand.Execute(null);

        }
    }
}
