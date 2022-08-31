using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
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
            SearchPatientsWindow search = new SearchPatientsWindow();
            search.ShowDialog();

            if (search.SelectedPatientCompany == null) return;

            this.DataContext = new PatientRegistrationViewModel(search.SelectedPatientCompany.PatientId);
        }

        private void DateOfBirthDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (PatientRegistrationViewModel)DataContext;

            if (vm.UpdateAgeByDateOfBirthCommand.CanExecute(null))
                vm.UpdateAgeByDateOfBirthCommand.Execute(DateOfBirthDatePicker.SelectedDate);
        }

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = (PatientRegistrationViewModel)DataContext;

            if (vm.UpdateIsAgeEditedCommand.CanExecute(null))
                vm.UpdateIsAgeEditedCommand.Execute(null);
        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenderComboBox.SelectedItem != null && GenderComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                //int moduleId = Globals.MODULES.Where(m => m.ModuleName == "Patients").Select(m => m.Id).FirstOrDefault();
                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(null, SingleLineEntries.Gender);
                singleLineEntryWindow.ShowDialog();

                var vm = (PatientRegistrationViewModel)DataContext;
                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute(SingleLineEntries.Gender);
            }
        }

        private void CivilStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CivilStatusComboBox.SelectedItem != null && CivilStatusComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                //int moduleId = Globals.MODULES.Where(m => m.ModuleName == "Patients").Select(m => m.Id).FirstOrDefault();
                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(null, SingleLineEntries.CivilStatus);
                singleLineEntryWindow.ShowDialog();

                var vm = (PatientRegistrationViewModel)DataContext;
                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute(SingleLineEntries.CivilStatus);
            }
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
