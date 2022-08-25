using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PackageViewModel : BaseViewModel
    {
        private const string EntityName = "Package";

        PackagesBLL packagesBLL = new PackagesBLL();
        PackageServicesBLL packageServicesBLL = new PackageServicesBLL();
        ServicesBLL servicesBLL = new ServicesBLL();
        CompaniesBLL companiesBLL = new CompaniesBLL();

        #region Public Properties
        public Package Package { get; set; }

        public ObservableCollection<PackageServiceViewModel> PackageServices { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddPackageServiceCommand { get; set; }
        public ICommand RemovePackageServiceCommand { get; set; }
        public ICommand UpdatePackageServiceCommand { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }
        #endregion

        public PackageViewModel(long id)
        {
            List<Company> companies = companiesBLL.GetAllCompanies();
            companies.Insert(0, new Company() { Id = 0, CompanyName = "Walk-in" });
            this.Companies = new ObservableCollection<Company>(companies);

            if (id == 0)
            {
                this.Package = new Package() { Id = 0, PackageName = string.Empty, PackageDescription = string.Empty, Price = 0, IsActive = true, PackagePrice = "0.00" };
                this.SelectedCompany = this.Companies.First();
                this.PackageServices = new ObservableCollection<PackageServiceViewModel>();
            }
            else
            {
                this.Package = packagesBLL.GetPackage(id);
                this.Package.PackagePrice = this.Package.Price.ToString("0.00");
                this.SelectedCompany = this.Companies.Where(c => c.Id == (long)this.Package.CompanyId).FirstOrDefault();
                this.PackageServices = this.PackageServiceViewModelList(packageServicesBLL.GetPackageServicesByPackageId(id));
            }

            this.NewCommand = new RelayCommand(param => NewPackage());
            this.SaveCommand = new RelayCommand(param => SavePackage());
            this.DeleteCommand = new RelayCommand(param => DeletePackage());
            this.AddPackageServiceCommand = new RelayCommand(param => AddPackageService((PackageServiceViewModel)param));
            this.RemovePackageServiceCommand = new RelayCommand(param => RemovePackageService((PackageServiceViewModel)param));
            this.UpdatePackageServiceCommand = new RelayCommand(param => UpdatePackageService((PackageServiceViewModel)param));
        }

        #region Data Actions
        private void NewPackage()
        {
            this.Package.Id = 0;
            this.Package.PackageName = string.Empty;
            this.Package.PackageDescription = string.Empty;
            this.Package.Price = 0;
            this.Package.IsActive = true;
            this.SelectedCompany = this.Companies.First();
            this.Package.PackagePrice = "0.00";
            this.PackageServices = new ObservableCollection<PackageServiceViewModel>();
        }

        private void SavePackage()
        {
            this.Package.CompanyId =  this.SelectedCompany.Id;
            if (!this.Package.IsValid || this.PackageServices.Where(p => !p.PackageService.IsValid).Any())
            {
                string errorMessages = this.Package.ErrorMessages;
                errorMessages += string.Join("", this.PackageServices.Where(p => !p.PackageService.IsValid).Select(p => p.PackageService.ErrorMessages).ToList());
                this.NotificationMessages = errorMessages;
                return;
            }

            long id = this.Package.Id;
            if (packagesBLL.SaveWithPackageServices(this.Package, this.PackageServices.Select(p => p.PackageService).ToList(), ref id))
            {
                this.Package.Id = id;
                this.NotificationMessages = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.SaveFailed;
        }

        private void DeletePackage()
        {
            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this package?", EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Package.Id;
            this.Package.IsActive = false;
            if (packagesBLL.SavePackage(this.Package, ref id))
            {
                this.Package = packagesBLL.GetLatestPackage();
                this.NotificationMessages = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.DeleteFailed;
        }
        #endregion

        #region Private Methods
        private ObservableCollection<PackageServiceViewModel> PackageServiceViewModelList(List<PackageService> packageServices)
        {
            List<PackageServiceViewModel> packageServicesList = new List<PackageServiceViewModel>();
            foreach (PackageService packageService in packageServices)
                packageServicesList.Add(new PackageServiceViewModel() { PackageService = packageService, Service = servicesBLL.GetService(packageService.ServiceId) });

            return new ObservableCollection<PackageServiceViewModel>(packageServicesList);
        }

        private void AddPackageService(PackageServiceViewModel packageServiceVM)
        {
            if (packageServiceVM == null)
            {
                PackageServiceViewModel psvm = new PackageServiceViewModel();
                this.PackageServices.Add(psvm);
            }
            else
                this.PackageServices.Add(packageServiceVM);
        }

        private void RemovePackageService(PackageServiceViewModel packageServiceVM)
        {
            this.PackageServices.Remove(packageServiceVM);
        }

        private void UpdatePackageService(PackageServiceViewModel packageServiceVM)
        {
            int index = this.PackageServices.IndexOf(packageServiceVM);
            this.PackageServices[index] = packageServiceVM;
        }
        #endregion
    }
}
