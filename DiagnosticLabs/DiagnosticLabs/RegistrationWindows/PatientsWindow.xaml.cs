using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.RegistrationWindows
{
    /// <summary>
    /// Interaction logic for PatietntsWindow.xaml
    /// </summary>
    public partial class PatientsWindow : Window
    {
        public PatientsWindow()
        {
            InitializeComponent();
            this.DataContext = new PatientViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchPatientsWindow search = new SearchPatientsWindow();
            search.ShowDialog();

            if (search.SelectedPatientCompany == null) return;

            this.DataContext = new PatientViewModel(search.SelectedPatientCompany.PatientId);
        }

        private void DateOfBirthDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (PatientViewModel)DataContext;

            if (vm.UpdateAgeByDateOfBirthCommand.CanExecute(null))
                vm.UpdateAgeByDateOfBirthCommand.Execute(DateOfBirthDatePicker.SelectedDate);
        }

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = (PatientViewModel)DataContext;

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

                var vm = (PatientViewModel)DataContext;
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

                var vm = (PatientViewModel)DataContext;
                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute(SingleLineEntries.CivilStatus);
            }
        }
    }
}
