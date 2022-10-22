using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchCompaniesWindow.xaml
    /// </summary>
    public partial class SearchCompaniesWindow : Window
    {
        public Company SelectedCompany { get; set; }

        public SearchCompaniesWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchCompanyViewModel();
            NameFilterTextBox.Focus();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchCompanies();
        }

        private void CompaniesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectCompany();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectCompany();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                CompaniesDataGrid.Focus();
        }
        #endregion

        #region Data to/from UI
        private void SearchCompanies()
        {
            SearchCompanyViewModel vm = (SearchCompanyViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }

        private void SelectCompany()
        {
            if (CompaniesDataGrid.Items.Count == 0) return;

            SelectedCompany = (Company)CompaniesDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
