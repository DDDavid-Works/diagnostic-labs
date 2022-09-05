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
        }

        #region UI Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchPackageViewModel vm = (SearchPackageViewModel)this.DataContext;

            if (vm.SearchCommand.CanExecute(null))
                vm.SearchCommand.Execute(null);
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
        #endregion

        #region Data to/from UI
        private void SelectPackage()
        {
            if (PackagesDataGrid.Items.Count == 0) return;

            SelectedPackage = (Package)PackagesDataGrid.SelectedItem;
            this.Close();
        }
        #endregion
    }
}
