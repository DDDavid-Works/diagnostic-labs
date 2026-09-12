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
    /// Interaction logic for ClinicalChemistry1Window.xaml
    /// </summary>
    public partial class ClinicalChemistry1Window : Window
    {
        public ClinicalChemistry1Window()
        {
            InitializeComponent();
            this.DataContext = new ClinicalChemistry1ViewModel(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (ClinicalChemistry1ViewModel)DataContext;

            if (vm.GetPatientRegistrationCommand.CanExecute(null))
                vm.GetPatientRegistrationCommand.Execute(Globals.PATIENTREGISTRATIONIDTOINPUT);
        }

        private void TestComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TestComboBox.SelectedItem != null && TestComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (ClinicalChemistry1ViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.ClinicalChemistry1Test, false);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(SingleLineEntries.ClinicalChemistry1Test);
            }
        }

        private void ResultTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowSelectMultiLineEntryWindow(MultiLineEntries.ClinicalChemistry1Result);
        }

        private void SelectResultButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectMultiLineEntryWindow(MultiLineEntries.ClinicalChemistry1Result);
        }

        private void RemarksTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowSelectMultiLineEntryWindow(MultiLineEntries.ClinicalChemistry1Remarks);
        }

        private void SelectRemarksButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectMultiLineEntryWindow(MultiLineEntries.ClinicalChemistry1Remarks);
        }

        private void MedicalTechnologistComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MedicalTechnologistComboBox.SelectedItem != null && MedicalTechnologistComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (ClinicalChemistry1ViewModel)DataContext;

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
                var vm = (ClinicalChemistry1ViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.Pathologist, true);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(SingleLineEntries.Pathologist);
            }
        }

        #region Private Methods
        private void ShowSelectMultiLineEntryWindow(string fieldName)
        {
            var vm = (ClinicalChemistry1ViewModel)DataContext;

            MultiLineEntryWindow mlew = new MultiLineEntryWindow(vm.ModuleId, fieldName, null, false);
            mlew.ShowDialog();

            if (mlew.SelectedMultiLineEntry == null) return;

            switch (fieldName)
            {
                case MultiLineEntries.ClinicalChemistry1Result:
                    ResultTextBox.Text = mlew.SelectedMultiLineEntry.FieldValue;
                    break;
                case MultiLineEntries.ClinicalChemistry1Remarks:
                    RemarksTextBox.Text = mlew.SelectedMultiLineEntry.FieldValue;
                    break;
                default:
                    break;
            }
        }

        private void ToggleSetDefaultsUI(bool isSetDefaults)
        {
            this.Title = isSetDefaults ? "Clinical Chemistry 1 [SET DEFAULT MODE]" : "Clinical Chemistry 1";

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
            SearchLabResultsWindow search = new SearchLabResultsWindow(Modules.ClinicalChemistry1);
            search.ShowDialog();

            if (search.SelectedLabResult == null) return;

            this.DataContext = new ClinicalChemistry1ViewModel(search.SelectedLabResult.Id);
        }

        private void ActionToolbar_PrintCommand(object sender, RoutedEventArgs e)
        {
            var vm = (ClinicalChemistry1ViewModel)DataContext;

            if (vm.ClinicalChemistry1 != null)
            {
                PrintWindow print = new PrintWindow(Modules.ClinicalChemistry1, vm.ClinicalChemistry1.Id);
                print.ShowDialog();
            }
        }

        private void ActionToolbar_SetDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(true);

            var vm = (ClinicalChemistry1ViewModel)DataContext;

            if (vm.SetDefaultsCommand.CanExecute(null))
                vm.SetDefaultsCommand.Execute(null);
        }

        private void ActionToolbar_SaveDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(false);

            var vm = (ClinicalChemistry1ViewModel)DataContext;

            if (vm.SaveDefaultsCommand.CanExecute(null))
                vm.SaveDefaultsCommand.Execute(null);
        }

        private void ActionToolbar_CloseDefaultsCommand(object sender, RoutedEventArgs e)
        {
            ToggleSetDefaultsUI(false);

            var vm = (ClinicalChemistry1ViewModel)DataContext;

            if (vm.NewCommand.CanExecute(null))
                vm.NewCommand.Execute(null);
        }
        #endregion
    }
}
