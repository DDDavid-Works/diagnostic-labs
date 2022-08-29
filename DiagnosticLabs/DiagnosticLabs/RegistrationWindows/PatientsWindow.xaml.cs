using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabs.Constants;
using System.Windows;

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

            if (search.SelectedPatient == null) return;

            this.DataContext = new PatientViewModel(search.SelectedPatient.Id);
        }

        private void DateOfBirthDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var vm = (PatientViewModel)DataContext;

            if (vm.UpdateAgeByDateOfBirthCommand.CanExecute(null))
                vm.UpdateAgeByDateOfBirthCommand.Execute(DateOfBirthDatePicker.SelectedDate);
        }

        private void AgeTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var vm = (PatientViewModel)DataContext;

            if (vm.UpdateIsAgeEditedCommand.CanExecute(null))
                vm.UpdateIsAgeEditedCommand.Execute(null);
        }

        private void GenderComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GenderComboBox.SelectedItem != null && GenderComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                //int moduleId = Globals.MODULES.Where(m => m.ModuleName == "Patients").Select(m => m.Id).FirstOrDefault();
                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(null, "Gender");
                singleLineEntryWindow.ShowDialog();

                var vm = (PatientViewModel)DataContext;
                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute("Gender");
            }
        }

        private void CivilStatusComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CivilStatusComboBox.SelectedItem != null && CivilStatusComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                //int moduleId = Globals.MODULES.Where(m => m.ModuleName == "Patients").Select(m => m.Id).FirstOrDefault();
                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(null, "Civil Status");
                singleLineEntryWindow.ShowDialog();

                var vm = (PatientViewModel)DataContext;
                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute("Civil Status");
            }
        }
    }
}
