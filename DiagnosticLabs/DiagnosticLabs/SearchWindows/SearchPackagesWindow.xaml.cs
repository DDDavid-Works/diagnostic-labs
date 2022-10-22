using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchPackagesWindow.xaml
    /// </summary>
    public partial class SearchPackagesWindow : Window
    {
        public Package SelectedPackage { get; set; }

        public SearchPackagesWindow()
        {
            InitializeComponent();
            this.DataContext = new SearchPackageViewModel();
            NameFilterTextBox.Focus();
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPackages();
        }

        private void PackagesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectPackage();
        }

        private void OkCancelUserControl_OkCommand(object sender, RoutedEventArgs e)
        {
            SelectPackage();
        }

        private void OkCancelUserControl_CancelCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameFilterTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                PackagesDataGrid.Focus();
        }
        #endregion

        #region Data to/from UI
        private void SearchPackages()
        {
            SearchPackageViewModel vm = (SearchPackageViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(true);
        }

        private void SelectPackage()
        {
            if (PackagesDataGrid.Items.Count == 0) return;

            SelectedPackage = (Package)PackagesDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
