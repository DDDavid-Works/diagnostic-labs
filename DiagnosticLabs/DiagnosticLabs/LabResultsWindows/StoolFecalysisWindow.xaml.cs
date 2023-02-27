using DiagnosticLabs.Constants;
using DiagnosticLabs.EntryBuilderWindows;
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

        private void ColorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ColorComboBox.SelectedItem != null && ColorComboBox.SelectedItem.ToString() == Texts.NewEntry)
            {
                var vm = (StoolFecalysisViewModel)DataContext;

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.StoolFecalysisColor);
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

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(vm.ModuleId, SingleLineEntries.StoolFecalysisConsistency);
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

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(null, SingleLineEntries.MedicalTechnologist);
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

                SingleLineEntryWindow singleLineEntryWindow = new SingleLineEntryWindow(null, SingleLineEntries.Pathologist);
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

        #region Private Methods
        private void ShowSelectMNultiLineEntryWindow(string fieldName)
        {
            var vm = (StoolFecalysisViewModel)DataContext;

            SelectMultiLineEntryWindow smlew = new SelectMultiLineEntryWindow(vm.ModuleId, fieldName);
            smlew.ShowDialog();

            if (smlew.SelectedMultiLineEntry == null) return;

            switch (fieldName)
            {
                case MultiLineEntries.StoolFecalysisResult:
                    ResultTextBox.Text = smlew.SelectedMultiLineEntry.FieldValue;
                    break;
                case MultiLineEntries.StoolFecalysisRemarks:
                    RemarksTextBox.Text = smlew.SelectedMultiLineEntry.FieldValue;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
