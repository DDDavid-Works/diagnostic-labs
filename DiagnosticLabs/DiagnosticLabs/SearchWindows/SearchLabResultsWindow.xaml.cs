using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models.Views;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchLabResultsWindow.xaml
    /// </summary>
    public partial class SearchLabResultsWindow : Window
    {
        public LabResult SelectedLabResult { get; set; }

        public SearchLabResultsWindow(string service)
        {
            InitializeComponent();
            this.Title = "Search " + service;
            this.DataContext = new SearchLabResultViewModel(service);
            NameFilterTextBox.Focus();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchLabResults();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectLabResult();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                LabReultsDataGrid.Focus();
        }

        private void LabReultsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectLabResult();
        }
        #endregion

        #region Data to/from UI
        private void SearchLabResults()
        {
            SearchLabResultViewModel vm = (SearchLabResultViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }

        private void SelectLabResult()
        {
            if (LabReultsDataGrid.Items.Count == 0) return;

            SelectedLabResult = (LabResult)LabReultsDataGrid.SelectedItem;
            this.Close();
        }
        #endregion

    }
}
