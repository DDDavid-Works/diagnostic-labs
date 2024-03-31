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
using System;
using Newtonsoft.Json.Linq;

namespace DiagnosticLabs.ViewModels
{
    public class PackageViewModel : BaseViewModel
    {
        private const string _entityName = "Package";

        CommonFunctions _commonFunctions = new CommonFunctions();
        PackagesBLL _packagesBLL = new PackagesBLL();
        PackageServicesBLL _packageServicesBLL = new PackageServicesBLL();
        ServicesBLL _servicesBLL = new ServicesBLL();

        #region Public Properties
        public Package Package { get; set; }

        public ObservableCollection<PackageServiceViewModel> PackageServices { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddPackageServiceCommand { get; set; }
        public ICommand RemovePackageServiceCommand { get; set; }
        public ICommand UpdatePackageServiceCommand { get; set; }
        public ICommand UpdateAllPackageServicesCommand { get; set; }
        public ICommand UpdateIsPriceEditedCommand { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }
        #endregion

        public PackageViewModel(long id)
        {
            this.Companies = new ObservableCollection<Company>(_commonFunctions.CompaniesList(true));

            if (id == 0)
                NewPackage();
            else
            {
                this.Package = _packagesBLL.GetPackage(id);
                this.SelectedCompany = this.Companies.Where(c => c.Id == (long?)this.Package.CompanyId).FirstOrDefault();
                this.PackageServices = this.PackageServiceViewModelList(_packageServicesBLL.GetPackageServicesByPackageId(id));
            }

            this.NewCommand = new RelayCommand(param => NewPackage());
            this.SaveCommand = new RelayCommand(param => SavePackage());
            this.DeleteCommand = new RelayCommand(param => DeletePackage());
            this.AddPackageServiceCommand = new RelayCommand(param => AddPackageService((PackageServiceViewModel)param));
            this.RemovePackageServiceCommand = new RelayCommand(param => RemovePackageService((PackageServiceViewModel)param));
            this.UpdatePackageServiceCommand = new RelayCommand(param => UpdatePackageService((PackageServiceViewModel)param));
            this.UpdateAllPackageServicesCommand = new RelayCommand(param => UpdateAllPackageServices((List<PackageServiceViewModel>)param));
            this.UpdateIsPriceEditedCommand = new RelayCommand(param => UpdateIsPriceEdited((bool)param));
        }

        #region Data Actions
        private void NewPackage()
        {
            this.Package = _packagesBLL.NewPackage();
            this.SelectedCompany = this.Companies.First();
            this.PackageServices = new ObservableCollection<PackageServiceViewModel>();
            this.ClearNotificationMessages();
        }

        private void SavePackage()
        {
            this.Package.CompanyId = this.SelectedCompany.Id;
            if (!this.Package.IsValid || this.PackageServices.Where(p => !p.PackageService.IsValid).Any())
            {
                string errorMessages = this.Package.ErrorMessages;
                errorMessages += string.Join("", this.PackageServices.Where(p => !p.PackageService.IsValid).Select(p => p.PackageService.ErrorMessages).ToList());
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Package.Id;
            if (_packagesBLL.SaveWithPackageServices(this.Package, this.PackageServices.Select(p => p.PackageService).ToList(), ref id))
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

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Package.Id;
            this.Package.IsActive = false;
            if (_packagesBLL.SavePackage(this.Package, ref id))
            {
                this.Package = _packagesBLL.GetLatestPackage();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion

        #region Data Actions
        private ObservableCollection<PackageServiceViewModel> PackageServiceViewModelList(List<PackageService> packageServices)
        {
            List<PackageServiceViewModel> packageServicesList = new List<PackageServiceViewModel>();
            foreach (PackageService packageService in packageServices)
            {
                packageServicesList.Add(new PackageServiceViewModel() { 
                    PackageService = packageService,
                    Service = _servicesBLL.GetService(packageService.ServiceId)
                });
                packageService.PackageServicePrice = String.Format("{0:N}", packageService.Price);
                Service service = _servicesBLL.GetService(packageService.ServiceId);
                packageService.PackageServiceName = service.ServiceName;
            }

            return new ObservableCollection<PackageServiceViewModel>(packageServicesList);
        }

        private void AddPackageService(PackageServiceViewModel packageServiceVM)
        {
            if (packageServiceVM == null)
            {
                PackageServiceViewModel psvm = new PackageServiceViewModel();
                psvm.PackageService.PackageServicePrice = String.Format("{0:N}", psvm.Service.Price);
                psvm.PackageService.PackageServiceName = psvm.Service.ServiceName;

                this.PackageServices.Add(psvm);
            }
            else
            {
                if (this.PackageServices.Any(p => p.Service.Id == packageServiceVM.Service.Id))
                    this.NotificationMessage = Messages.PackageServiceExists;
                else
                    this.PackageServices.Add(packageServiceVM);
            }

            ComputeTotalPrice();
        }

        private void RemovePackageService(PackageServiceViewModel packageServiceVM)
        {
            this.PackageServices.Remove(packageServiceVM);

            ComputeTotalPrice();
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

            ComputeTotalPrice();
        }

        private void UpdateAllPackageServices(List<PackageServiceViewModel> packageServiceVMs)
        {
            this.PackageServices.Clear();
            this.PackageServices = new ObservableCollection<PackageServiceViewModel>(packageServiceVMs);

            ComputeTotalPrice();
        }

        public void UpdateIsPriceEdited(bool isValueChanged)
        {
            if (!isValueChanged) return;

            this.Package.IsPriceEdited = true;
        }
        #endregion

        #region Private Methods
        private void ComputeTotalPrice()
        {
            if (!this.Package.IsPriceEdited)
            {
                decimal price = this.PackageServices.Select(p => p.PackageService.Price).Sum();
                this.Package.Price = price;
                this.Package.PackagePrice = String.Format("{0:N}", price);
                this.Package.IsPriceEdited = false;
            }
        }
        #endregion
    }
}
