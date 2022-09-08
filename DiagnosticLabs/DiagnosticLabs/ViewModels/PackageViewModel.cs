using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Internal;

namespace DiagnosticLabs.ViewModels
{
    public class PackageViewModel : BaseViewModel
    {
        private const string EntityName = "Package";

        CommonFunctions commonFunctions = new CommonFunctions();
        PackagesBLL packagesBLL = new PackagesBLL();
        PackageServicesBLL packageServicesBLL = new PackageServicesBLL();
        ServicesBLL servicesBLL = new ServicesBLL();

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
            this.Companies = new ObservableCollection<Company>(commonFunctions.CompaniesList(true));

            if (id == 0)
                NewPackage();
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
            if (this.Package == null)
                this.Package = new Package();

            this.Package.Id = 0;
            this.Package.PackageName = string.Empty;
            this.Package.PackageDescription = string.Empty;
            this.Package.Price = 0;
            this.Package.IsActive = true;
            this.SelectedCompany = this.Companies.First();
            this.Package.PackagePrice = "0.00";
            this.PackageServices = new ObservableCollection<PackageServiceViewModel>();
            this.ClearNotificationMessages();
        }

        private void SavePackage()
        {
            if (!this.Package.IsValid || this.PackageServices.Where(p => !p.PackageService.IsValid).Any())
            {
                string errorMessages = this.Package.ErrorMessages;
                errorMessages += string.Join("", this.PackageServices.Where(p => !p.PackageService.IsValid).Select(p => p.PackageService.ErrorMessages).ToList());
                this.NotificationMessage = commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Package.Id;
            if (packagesBLL.SaveWithPackageServices(this.Package, this.PackageServices.Select(p => p.PackageService).ToList(), ref id))
            {
                this.Package.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeletePackage()
        {
            if (this.Package.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Package.Id;
            this.Package.IsActive = false;
            if (packagesBLL.SavePackage(this.Package, ref id))
            {
                this.Package = packagesBLL.GetLatestPackage();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
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
            {
                if (this.PackageServices.Any(p => p.Service.Id == packageServiceVM.Service.Id))
                    this.NotificationMessage = Messages.PackageServiceExists;
                else
                    this.PackageServices.Add(packageServiceVM);
            }
        }

        private void RemovePackageService(PackageServiceViewModel packageServiceVM)
        {
            this.PackageServices.Remove(packageServiceVM);
        }

        private void UpdatePackageService(PackageServiceViewModel packageServiceVM)
        {
            if (this.PackageServices.Any(p => p.Service.Id == packageServiceVM.Service.Id))
                this.NotificationMessage = Messages.PackageServiceExists;
            else
            {
                int index = this.PackageServices.IndexOf(packageServiceVM);
                this.PackageServices[index] = packageServiceVM;
            }
        }
        #endregion
    }
}
