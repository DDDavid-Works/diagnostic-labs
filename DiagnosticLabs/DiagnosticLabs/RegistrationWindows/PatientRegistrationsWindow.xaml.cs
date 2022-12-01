using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.RegistrationWindows
{
    /// <summary>
    /// Interaction logic for PatientRegistrationsWindow.xaml
    /// </summary>
    public partial class PatientRegistrationsWindow : Window
    {
        public PatientRegistrationsWindow()
        {
            InitializeComponent();
            this.DataContext = new PatientRegistrationViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchPatientRegistrationsWindow search = new SearchPatientRegistrationsWindow();
            search.ShowDialog();

            if (search.SelectedPatientRegistrationDetail == null) return;

            this.DataContext = new PatientRegistrationViewModel(search.SelectedPatientRegistrationDetail.PatientRegistrationId);
        }

        private void PatientRegistrationPriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var vm = (PatientRegistrationViewModel)DataContext;

            if (vm.UpdateIsPriceEditedCommand.CanExecute(null))
                vm.UpdateIsPriceEditedCommand.Execute(null);
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            SearchServicesWindow search = new SearchServicesWindow();
            search.ShowDialog();

            if (search.SelectedService == null) return;

            var vm = (PatientRegistrationViewModel)DataContext;
            if (vm.AddPatientRegistrationServiceCommand.CanExecute(null))
            {
                PatientRegistrationServiceViewModel prsvm = new PatientRegistrationServiceViewModel()
                {
                    PatientRegistrationService = new PatientRegistrationService()
                    {
                        PatientRegistrationId = vm.PatientRegistration.Id,
                        ServiceId = search.SelectedService.Id,
                        Price = search.SelectedService.Price, IsActive = true
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
    }
}
