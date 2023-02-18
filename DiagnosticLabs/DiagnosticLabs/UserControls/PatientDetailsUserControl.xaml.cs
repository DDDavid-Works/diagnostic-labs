using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for PatientDetailsUserControl.xaml
    /// </summary>
    public partial class PatientDetailsUserControl : UserControl
    {
        PatientsBLL patientsBLL = new PatientsBLL();

        public static readonly DependencyProperty ShowSearchButtonProperty =
            DependencyProperty.Register("ShowSearchButton", typeof(bool), typeof(PatientDetailsUserControl), new PropertyMetadata(true, new PropertyChangedCallback(OnShowSearchButtonChanged)));

        public bool ShowSearchButton
        {
            get { return (bool)GetValue(ShowSearchButtonProperty); }
            set { SetValue(ShowSearchButtonProperty, value); }
        }

        public PatientDetailsUserControl()
        {
            InitializeComponent();
        }

        private void DateOfBirthDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (BasePatientViewModel)DataContext;

            if (vm.UpdateAgeByDateOfBirthCommand.CanExecute(null))
                vm.UpdateAgeByDateOfBirthCommand.Execute(DateOfBirthDatePicker.SelectedDate);

            //if (DataContext.GetType().Equals(typeof(PatientRegistrationViewModel)))
            //{
            //    var vm = (PatientRegistrationViewModel)DataContext;

            //    if (vm.UpdateAgeByDateOfBirthCommand.CanExecute(null))
            //        vm.UpdateAgeByDateOfBirthCommand.Execute(DateOfBirthDatePicker.SelectedDate);
            //}
            //else if (DataContext.GetType().Equals(typeof(PatientViewModel)))
            //{
            //    var vm = (PatientViewModel)DataContext;

            //    if (vm.UpdateAgeByDateOfBirthCommand.CanExecute(null))
            //        vm.UpdateAgeByDateOfBirthCommand.Execute(DateOfBirthDatePicker.SelectedDate);
            //}
            //else if (DataContext.GetType().Equals(typeof(PaymentViewModel)))
            //{
            //    var vm = (PaymentViewModel)DataContext;

            //    if (vm.UpdateAgeByDateOfBirthCommand.CanExecute(null))
            //        vm.UpdateAgeByDateOfBirthCommand.Execute(DateOfBirthDatePicker.SelectedDate);
            //}
        }

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext.GetType().Equals(typeof(PatientViewModel)))
            {
                var vm = (PatientViewModel)DataContext;

                if (vm.UpdateIsAgeEditedCommand.CanExecute(null))
                    vm.UpdateIsAgeEditedCommand.Execute(null);
            }
            else
            {
                var vm = (BasePatientViewModel)DataContext;

                if (vm.UpdateIsAgeEditedCommand.CanExecute(null))
                    vm.UpdateIsAgeEditedCommand.Execute(null);
            }
        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenderComboBox.SelectedItem != null && GenderComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(null, SingleLineEntries.Gender);
                singleLineEntryWindow.ShowDialog();

                var vm = (BasePatientViewModel)DataContext;
                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute(SingleLineEntries.Gender);
            }
        }

        private void CivilStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CivilStatusComboBox.SelectedItem != null && CivilStatusComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(null, SingleLineEntries.CivilStatus);
                singleLineEntryWindow.ShowDialog();

                var vm = (BasePatientViewModel)DataContext;
                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute(SingleLineEntries.CivilStatus);
            }
        }

        private void SearchPatientsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientsWindow search = new SearchPatientsWindow();
            search.ShowDialog();

            if (search.SelectedPatientCompany == null) return;

            ((BasePatientViewModel)DataContext).Patient = patientsBLL.GetPatient(search.SelectedPatientCompany.PatientId);
        }

        private static void OnShowSearchButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PatientDetailsUserControl patientDetailsUserControl = d as PatientDetailsUserControl;
            patientDetailsUserControl.OnShowSearchButtonChanged(e);
        }

        private void OnShowSearchButtonChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.ToString().ToLower() == "true")
            {
                PatientNameTextBox.SetValue(Grid.ColumnSpanProperty, 3);
                SearchPatientsButton.Visibility = Visibility.Visible;
            }
            else
            {
                PatientNameTextBox.SetValue(Grid.ColumnSpanProperty, 4);
                SearchPatientsButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
