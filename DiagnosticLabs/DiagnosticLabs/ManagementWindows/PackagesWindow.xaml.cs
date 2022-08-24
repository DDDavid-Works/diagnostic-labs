using DiagnosticLabs.SearchWindows;
using DiagnosticLabs.ViewModels;
using DiagnosticLabsDAL.Models;
using System.Windows;

namespace DiagnosticLabs.ManagementWindows
{
    /// <summary>
    /// Interaction logic for PackagesWindow.xaml
    /// </summary>
    public partial class PackagesWindow : Window
    {
        public PackagesWindow()
        {
            InitializeComponent();
            this.DataContext = new PackageViewModel(0);
        }

        private void ActionToolbar_SearchCommand(object sender, RoutedEventArgs e)
        {
            SearchPackagesWindow search = new SearchPackagesWindow();
            search.ShowDialog();

            if (search.SelectedPackage == null) return;

            this.DataContext = new PackageViewModel(search.SelectedPackage.Id);
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            SearchServicesWindow search = new SearchServicesWindow();
            search.ShowDialog();

            if (search.SelectedService == null) return;

            var vm = (PackageViewModel)DataContext;
            if (vm.AddPackageServiceCommand.CanExecute(null))
            {
                PackageServiceViewModel psvm = new PackageServiceViewModel()
                {
                    PackageService = new PackageService() { PackageId = vm.Package.Id, ServiceId = search.SelectedService.Id, Price = search.SelectedService.Price, IsActive = true },
                    Service = search.SelectedService
                };
                vm.AddPackageServiceCommand.Execute(psvm);
            }
        }

        private void EditServiceButton_Click(object sender, RoutedEventArgs e)
        {
            SearchServicesWindow search = new SearchServicesWindow();
            search.ShowDialog();

            if (search.SelectedService == null) return;

            var vm = (PackageViewModel)DataContext;
            if (vm.UpdatePackageServiceCommand.CanExecute(null))
            {
                PackageServiceViewModel psvm = (PackageServiceViewModel)((System.Windows.Controls.Button)sender).CommandParameter;
                psvm.PackageService = new PackageService() { PackageId = vm.Package.Id, ServiceId = search.SelectedService.Id, Price = search.SelectedService.Price, IsActive = true };
                psvm.Service = search.SelectedService;
                vm.UpdatePackageServiceCommand.Execute(psvm);
            }
        }
    }
}
