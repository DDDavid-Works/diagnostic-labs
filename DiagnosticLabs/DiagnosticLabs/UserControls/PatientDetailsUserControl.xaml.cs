using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabs.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;

namespace DiagnosticLabs.UserControls
{
    /// <summary>
    /// Interaction logic for PatientDetailsUserControl.xaml
    /// </summary>
    public partial class PatientDetailsUserControl : UserControl
    {
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
                var vm = (BasePatientViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.Gender, true);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute(SingleLineEntries.Gender);
            }
        }

        private void CivilStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CivilStatusComboBox.SelectedItem != null && CivilStatusComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (BasePatientViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.CivilStatus, true);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshSingleLineEntryListCommand.Execute(SingleLineEntries.CivilStatus);
            }
        }

        private static void OnShowSearchButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //PatientDetailsUserControl patientDetailsUserControl = d as PatientDetailsUserControl;
            //patientDetailsUserControl.ShowSearchButton = e.ToString();
        }
    }
}
