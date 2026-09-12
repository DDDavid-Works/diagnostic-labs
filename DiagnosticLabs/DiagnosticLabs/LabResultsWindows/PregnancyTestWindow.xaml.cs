using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.PrintWindows;
using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Globals;
using System.Windows;
using System.Windows.Media;

namespace DiagnosticLabs.LabResultsWindows
{
    /// <summary>
    /// Interaction logic for PregnancyTestWindow.xaml
    /// </summary>
    public partial class PregnancyTestWindow : Window
    {
        public PregnancyTestWindow()
        {
            InitializeComponent();
            this.DataContext = new PregnancyTestViewModel(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (PregnancyTestViewModel)DataContext;

            if (vm.GetPatientRegistrationCommand.CanExecute(null))
                vm.GetPatientRegistrationCommand.Execute(Globals.PATIENTREGISTRATIONIDTOINPUT);
        }

        private void ResultTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowSelectMultiLineEntryWindow(MultiLineEntries.StoolFecalysisResult);
        }

        private void SelectResultButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectMultiLineEntryWindow(MultiLineEntries.StoolFecalysisResult);
        }

        private void RemarksTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowSelectMultiLineEntryWindow(MultiLineEntries.StoolFecalysisRemarks);
        }

        private void SelectRemarksButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectMultiLineEntryWindow(MultiLineEntries.StoolFecalysisRemarks);
        }

        private void MedicalTechnologistComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MedicalTechnologistComboBox.SelectedItem != null && MedicalTechnologistComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (PregnancyTestViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.MedicalTechnologist, true);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(SingleLineEntries.MedicalTechnologist);
            }
        }

        private void PathologistComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (PathologistComboBox.SelectedItem != null && PathologistComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (PregnancyTestViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.Pathologist, true);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(SingleLineEntries.Pathologist);
            }
        }

        #region Private Methods
        private void ShowSelectMultiLineEntryWindow(string fieldName)
        {
            var vm = (PregnancyTestViewModel)DataContext;

            MultiLineEntryWindow mlew = new MultiLineEntryWindow(vm.ModuleId, fieldName, null, false);
            mlew.ShowDialog();

            if (mlew.SelectedMultiLineEntry == null) return;

            switch (fieldName)
            {
                case MultiLineEntries.PregnancyTestResult:
                    ResultTextBox.Text = mlew.SelectedMultiLineEntry.FieldValue;
                    break;
                case MultiLineEntries.PregnancyTestRemarks:
                    RemarksTextBox.Text = mlew.SelectedMultiLineEntry.FieldValue;
                    break;
                default:
                    break;
            }
        }

        private void ToggleSetDefaultsUI(bool isSetDefaults)
        {
            this.Title = isSetDefaults ? "Pregnancy Test [SET DEFAULT MODE]" : "Pregnancy Test";

            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom(isSetDefaults ? "#F0E495" : "#DFECDF");

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
            PatientSearchByCode.IsEnabled = !isSetDefaults;
            PatientSearchByName.IsEnabled = !isSetDefaults;
            CompanyOrPhysicianTextBox.IsEnabled = !isSetDefaults;
            AgeTextBox.IsEnabled = !isSetDefaults;
            SexTextBox.IsEnabled = !isSetDefaults;
            DateRequestedTextBox.IsEnabled = !isSetDefaults;
        }
        #endregion

        #region Action Toolbar Actions
        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchLabResultsWindow search = new SearchLabResultsWindow(Modules.PregnancyTest);
            search.ShowDialog();

            if (search.SelectedLabResult == null) return;

            this.DataContext = new PregnancyTestViewModel(search.SelectedLabResult.Id);
        }

        private void ActionToolbar_PrintCommand(object sender, RoutedEventArgs e)
        {
            var vm = (PregnancyTestViewModel)DataContext;

            if (vm.PregnancyTest != null)
            {
                PrintWindow print = new PrintWindow(Modules.PregnancyTest, vm.PregnancyTest.Id);
                print.ShowDialog();
            }
        }

        private void ActionToolbar_SetDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(true);

            var vm = (PregnancyTestViewModel)DataContext;

            if (vm.SetDefaultsCommand.CanExecute(null))
                vm.SetDefaultsCommand.Execute(null);
        }

        private void ActionToolbar_SaveDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(false);

            var vm = (PregnancyTestViewModel)DataContext;

            if (vm.SaveDefaultsCommand.CanExecute(null))
                vm.SaveDefaultsCommand.Execute(null);
        }

        private void ActionToolbar_CloseDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(false);

            var vm = (PregnancyTestViewModel)DataContext;

            if (vm.NewCommand.CanExecute(null))
                vm.NewCommand.Execute(null);
        }
        #endregion
    }
}
