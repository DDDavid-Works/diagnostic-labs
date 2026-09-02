using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.PrintWindows;
using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Globals;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiagnosticLabs.LabResultsWindows
{
    /// <summary>
    /// Interaction logic for MedicalExaminationWindow.xaml
    /// </summary>
    public partial class MedicalExaminationWindow : Window
    {
        public MedicalExaminationWindow()
        {
            InitializeComponent();
            this.DataContext = new MERViewModel(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (MERViewModel)DataContext;

            if (vm.GetPatientRegistrationCommand.CanExecute(null))
                vm.GetPatientRegistrationCommand.Execute(Globals.PATIENTREGISTRATIONIDTOINPUT);
        }

        private void MERCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            string field = checkBox.Name.Replace("NCheckBox", string.Empty).Replace("FCheckBox", string.Empty);
            string value = checkBox.Name.Replace(field, string.Empty).Replace("CheckBox", string.Empty);

            if (checkBox.IsChecked != null && checkBox.IsChecked == true)
            {
                string reverseValue = value == "F" ? "N" : "F";
                CheckBox reverseCheckBox = this.FindName(field + reverseValue + "CheckBox") as CheckBox;
                reverseCheckBox.IsChecked = false;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var vm = (MERViewModel)DataContext;
            string fieldName = comboBox.Tag.ToString();

            if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, fieldName, false);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(fieldName);
            }
        }

        #region Private Methods
        private void ToggleSetDefaultsUI(bool isSetDefaults)
        {
            base.Title = isSetDefaults ? "Medical Examination Report [SET DEFAULT MODE]" : "Medical Examination Report";

            var bc = new BrushConverter();
            base.Background = (Brush)bc.ConvertFrom(isSetDefaults ? "#F0E495" : "#DFECDF");

            Visibility isVisible = isSetDefaults ? Visibility.Collapsed : Visibility.Visible,
                isHidden = isSetDefaults ? Visibility.Visible : Visibility.Hidden;

            ActionToolbar.NewButtonBox.Visibility = isVisible;
            ActionToolbar.SaveButtonBox.Visibility = isVisible;
            ActionToolbar.DeleteButtonBox.Visibility = isVisible;
            ActionToolbar.PrintButtonBox.Visibility = isVisible;
            ActionToolbar.SearchButtonBox.Visibility = isVisible;
            ActionToolbar.SetDefaultsButtonBox.Visibility = isVisible;
            ActionToolbar.SaveDefaultsButtonBox.Visibility = isHidden;
            ActionToolbar.CloseDefaultsButtonBox.Visibility = isHidden;

            PatientRegistrationSearchByCode.IsEnabled = !isSetDefaults;
            InputDateDatePicker.IsEnabled = !isSetDefaults;
            CompanyNameTextBox.IsEnabled = !isSetDefaults;
            BatchNameTextBox.IsEnabled = !isSetDefaults;

            PatientSearchByName.IsEnabled = !isSetDefaults;
            ContactNumberTextBox.IsEnabled = !isSetDefaults;
            AgeTextBox.IsEnabled = !isSetDefaults;
            GenderComboBox.IsEnabled = !isSetDefaults;
            CivilStatusComboBox.IsEnabled = !isSetDefaults;
            CompanyTextBox.IsEnabled = !isSetDefaults;
        }
        #endregion

        #region Action Toolbar Actions
        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchLabResultsWindow search = new SearchLabResultsWindow(Modules.MedicalExamination);
            search.ShowDialog();

            if (search.SelectedLabResult == null) return;

            this.DataContext = new MERViewModel(search.SelectedLabResult.Id);
        }

        private void ActionToolbar_PrintCommand(object sender, RoutedEventArgs e)
        {
            var vm = (MERViewModel)DataContext;

            if (vm.MER != null)
            {
                PrintWindow print = new PrintWindow(Modules.MedicalExamination, vm.MER.Id);
                print.ShowDialog();
            }
        }

        private void ActionToolbar_SetDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(true);

            var vm = (MERViewModel)DataContext;

            if (vm.SetDefaultsCommand.CanExecute(null))
                vm.SetDefaultsCommand.Execute(null);
        }

        private void ActionToolbar_SaveDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(false);

            var vm = (MERViewModel)DataContext;

            if (vm.SaveDefaultsCommand.CanExecute(null))
                vm.SaveDefaultsCommand.Execute(null);
        }

        private void ActionToolbar_CloseDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(false);

            var vm = (MERViewModel)DataContext;

            if (vm.NewCommand.CanExecute(null))
                vm.NewCommand.Execute(null);
        }
        #endregion
    }
}
