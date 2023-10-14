using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
using DiagnosticLabs.PrintWindows;
using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Globals;
using System.Windows;

namespace DiagnosticLabs.LabResultsWindows
{
    /// <summary>
    /// Interaction logic for StoolFecalysisWindow.xaml
    /// </summary>
    public partial class StoolFecalysisWindow : Window
    {
        public StoolFecalysisWindow()
        {
            InitializeComponent();
            this.DataContext = new StoolFecalysisViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchLabResultsWindow search = new SearchLabResultsWindow(Modules.StoolFecalysis);
            search.ShowDialog();

            if (search.SelectedLabResult == null) return;

            this.DataContext = new StoolFecalysisViewModel(search.SelectedLabResult.Id);
        }

        private void ActionToolbar_PrintCommand(object sender, RoutedEventArgs e)
        {
            var vm = (StoolFecalysisViewModel)DataContext;

            if (vm.StoolFecalysis != null)
            {
                PrintWindow print = new PrintWindow(Modules.StoolFecalysis, vm.StoolFecalysis.Id);
                print.ShowDialog();
            }
        }

        private void ColorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ColorComboBox.SelectedItem != null && ColorComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (StoolFecalysisViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.StoolFecalysisColor, false);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(SingleLineEntries.StoolFecalysisColor);
            }
        }

        private void ConsistencyComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ConsistencyComboBox.SelectedItem != null && ConsistencyComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (StoolFecalysisViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.StoolFecalysisConsistency, false);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(SingleLineEntries.StoolFecalysisConsistency);
            }
        }

        private void MedicalTechnologistComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MedicalTechnologistComboBox.SelectedItem != null && MedicalTechnologistComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (StoolFecalysisViewModel)DataContext;

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
                var vm = (StoolFecalysisViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.Pathologist, true);
                singleLineEntryWindow.ShowDialog();

                if (vm.RefreshLabResultsSingleLineEntryListCommand.CanExecute(null))
                    vm.RefreshLabResultsSingleLineEntryListCommand.Execute(SingleLineEntries.Pathologist);
            }
        }

        private void ResultTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowSelectMNultiLineEntryWindow(MultiLineEntries.StoolFecalysisResult);
        }

        private void SelectResultButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectMNultiLineEntryWindow(MultiLineEntries.StoolFecalysisResult);
        }

        private void RemarksTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowSelectMNultiLineEntryWindow(MultiLineEntries.StoolFecalysisRemarks);
        }

        private void SelectRemarksButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectMNultiLineEntryWindow(MultiLineEntries.StoolFecalysisRemarks);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (StoolFecalysisViewModel)DataContext;

            if (vm.GetPatientRegistrationCommand.CanExecute(null))
                vm.GetPatientRegistrationCommand.Execute(Globals.PATIENTREGISTRATIONIDTOINPUT);
        }

        #region Private Methods
        private void ShowSelectMNultiLineEntryWindow(string fieldName)
        {
            var vm = (StoolFecalysisViewModel)DataContext;

            MultiLineEntryWindow mlew = new MultiLineEntryWindow(vm.ModuleId, fieldName, null, false);
            mlew.ShowDialog();

            if (mlew.SelectedMultiLineEntry == null) return;

            switch (fieldName)
            {
                case MultiLineEntries.StoolFecalysisResult:
                    ResultTextBox.Text = mlew.SelectedMultiLineEntry.FieldValue;
                    break;
                case MultiLineEntries.StoolFecalysisRemarks:
                    RemarksTextBox.Text = mlew.SelectedMultiLineEntry.FieldValue;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
