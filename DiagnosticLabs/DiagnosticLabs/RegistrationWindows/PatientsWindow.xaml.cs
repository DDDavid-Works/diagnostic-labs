using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
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
    }
}
