using DiagnosticLabs.ViewModels;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.SearchWindows
{
    /// <summary>
    /// Interaction logic for SearchPackagesWindow.xaml
    /// </summary>
    public partial class SearchPackagesWindow : Window
    {
        PackagesBLL packagesBLL = new PackagesBLL();

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
            long companyId = vm.SelectedCompany != null ? vm.SelectedCompany.Id : 0;
            LoadPackageList(NameFilterTextBox.Text, companyId);
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
        private void LoadPackageList(string name = null, long? companyId = 0)
        {
            PackagesDataGrid.ItemsSource = PackageList(name, companyId);
            PackagesDataGrid.Items.Refresh();
        }

        private List<Package> PackageList(string name, long? companyId)
        {
            return packagesBLL.GetPackages(name, companyId);
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
