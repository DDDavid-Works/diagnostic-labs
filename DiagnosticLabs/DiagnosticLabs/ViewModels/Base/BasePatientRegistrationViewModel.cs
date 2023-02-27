using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels.Base
{
    public class BasePatientRegistrationViewModel : BasePatientViewModel
    {
        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();
        PatientRegistrationServicesBLL _patientRegistrationServicesBLL = new PatientRegistrationServicesBLL();
        PatientsBLL _patientsBLL = new PatientsBLL();
        ServicesBLL _servicesBLL = new ServicesBLL();
        PackagesBLL _packagesBLL = new PackagesBLL();
        PackageServicesBLL _packageServicesBLL = new PackageServicesBLL();

        bool isLoading = false;

        #region Public Properties
        public PatientRegistration PatientRegistration { get; set; }
        public PatientRegistrationPayment PatientRegistrationPayment { get; set; }

        public ICommand AddPatientRegistrationServiceCommand { get; set; }
        public ICommand RemovePatientRegistrationServiceCommand { get; set; }
        public ICommand UpdatePatientRegistrationServiceCommand { get; set; }
        public ICommand UpdateAllPatientRegistrationServicesCommand { get; set; }
        public ICommand UpdateIsPriceEditedCommand { get; set; }
        public ICommand RefreshPatientRegistrationBatchCommand { get; set; }
        public ICommand GetPatientRegistrationCommand { get; set; }
        public ICommand LoadPatientRegistrationCommand { get; set; }

        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<Package> Packages { get; set; }
        public ObservableCollection<PatientRegistrationBatch> PatientRegistrationBatches { get; set; }
        public ObservableCollection<PatientRegistrationServiceViewModel> PatientRegistrationServices { get; set; }

        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }

        private Package _SelectedPackage;
        public Package SelectedPackage
        {
            get { return _SelectedPackage; }
            set
            {
                _SelectedPackage = value;
                OnPropertyChanged("SelectedPackage");
                this.LoadPackageServices();
            }
        }

        private string _SelectedBatchName;
        public string SelectedBatchName
        {
            get { return _SelectedBatchName; }
            set { _SelectedBatchName = value; OnPropertyChanged("SelectedBatchName"); }
        }
        #endregion

        public BasePatientRegistrationViewModel()
        {
            this.LoadPatientRegistrationComboBoxes();

            this.AddPatientRegistrationServiceCommand = new RelayCommand(param => AddPatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.RemovePatientRegistrationServiceCommand = new RelayCommand(param => RemovePatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.UpdatePatientRegistrationServiceCommand = new RelayCommand(param => UpdatePatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.UpdateAllPatientRegistrationServicesCommand = new RelayCommand(param => UpdateAllPatientRegistrationService((List<PatientRegistrationServiceViewModel>)param));
            this.UpdateIsPriceEditedCommand = new RelayCommand(param => UpdateIsPriceEdited((bool)param));
            this.RefreshPatientRegistrationBatchCommand = new RelayCommand(param => RefreshComboBoxesByCompanyId(false));
            this.GetPatientRegistrationCommand = new RelayCommand(param => GetPatientRegistration((long)param));
            this.LoadPatientRegistrationCommand = new RelayCommand(param => LoadPatientRegistration((long)param));
        }

        #region Data Actions
        public virtual void SavePatientRegistration()
        {
            if (this.SelectedPackage == null || this.SelectedPackage.Id == 0)
                this.PatientRegistration.PackageId = null;
            else
                this.PatientRegistration.PackageId = this.SelectedPackage.Id;

            this.PatientRegistration.CompanyId = this.SelectedCompany?.Id;
            this.PatientRegistration.BatchName = this.SelectedBatchName == null ? string.Empty : this.SelectedBatchName;
        }

        private void AddPatientRegistrationService(PatientRegistrationServiceViewModel patientRegistrationServiceVM)
        {
            if (patientRegistrationServiceVM == null)
            {
                PatientRegistrationServiceViewModel prsvm = new PatientRegistrationServiceViewModel();
                this.PatientRegistrationServices.Add(prsvm);
            }
            else
                this.PatientRegistrationServices.Add(patientRegistrationServiceVM);

            ComputeTotalPrice();
        }

        private void RemovePatientRegistrationService(PatientRegistrationServiceViewModel patientRegistrationServiceVM)
        {
            this.PatientRegistrationServices.Remove(patientRegistrationServiceVM);

            ComputeTotalPrice();
        }

        private void UpdatePatientRegistrationService(PatientRegistrationServiceViewModel patientRegistrationServiceVM)
        {
            int index = this.PatientRegistrationServices.IndexOf(patientRegistrationServiceVM);
            this.PatientRegistrationServices[index] = patientRegistrationServiceVM;

            ComputeTotalPrice();
        }

        private void UpdateAllPatientRegistrationService(List<PatientRegistrationServiceViewModel> patientRegistrationServiceVMs)
        {
            this.PatientRegistrationServices.Clear();
            this.PatientRegistrationServices = new ObservableCollection<PatientRegistrationServiceViewModel>(patientRegistrationServiceVMs);

            ComputeTotalPrice();
        }

        public void UpdateIsPriceEdited(bool isValueChanged)
        {
            if (!isValueChanged) return;

            this.PatientRegistration.IsPriceEdited = true;
        }

        private void RefreshComboBoxesByCompanyId(bool isInit)
        {
            if (isInit) return;

            long? companyId = this.SelectedCompany?.Id;
            this.PatientRegistrationBatches = new ObservableCollection<PatientRegistrationBatch>(_commonFunctions.PatientRegistrationBatchList(companyId));
            this.Packages = new ObservableCollection<Package>(_packagesBLL.GetPackagesByCompanyId(companyId, true));
        }

        public virtual void GetPatientRegistration(long patientRegistrationId)
        {
            if (patientRegistrationId == 0) return;

            this.LoadPatientRegistration(patientRegistrationId);
        }

        public void LoadPatientRegistration(long id)
        {
            this.isLoading = true;
            this.PatientRegistration = _patientRegistrationsBLL.GetPatientRegistration(id);
            this.PatientRegistration.IsPriceEdited = true;
            this.Patient = _patientsBLL.GetPatient((long)PatientRegistration.PatientId);
            this.Patient.IsAgeEdited = true;
            this.PatientRegistrationPayment = _patientRegistrationsBLL.GetPatientRegistrationPayment(id);

            this.SelectedCompany = this.Companies.Where(c => c.Id == (this.PatientRegistration.CompanyId == null ? 0 : this.PatientRegistration.CompanyId)).FirstOrDefault();
            this.SelectedPackage = this.Packages.Where(p => p.Id == (this.PatientRegistration.PackageId == null ? 0 : this.PatientRegistration.PackageId)).FirstOrDefault();
            this.SelectedBatchName = this.PatientRegistration.BatchName;

            this.PatientRegistrationServices = PatientRegistrationServiceViewModelList(_patientRegistrationServicesBLL.GetPatientRegistrationServicesByPatientRegistrationId(id));
            this.isLoading = false;
        }

        public virtual void LoadPackageServices()
        {
            if (this.isLoading) return;

            if (this.PatientRegistration != null)
            {
                this.PatientRegistration.AmountDue = 0;
                this.PatientRegistration.PatientRegistrationAmountDue = "0.00";
                this.PatientRegistration.IsPriceEdited = false;
            }

            if (this.SelectedPackage == null) return;

            if (this.SelectedPackage.Id != 0)
            {
                Package package = _packagesBLL.GetPackage(this.SelectedPackage.Id);
                this.PatientRegistration.AmountDue = package.Price;
                this.PatientRegistration.PatientRegistrationAmountDue = String.Format("{0:N}", package.Price);
                this.PatientRegistration.IsPriceEdited = true;
            }

            this.PatientRegistrationServices = new ObservableCollection<PatientRegistrationServiceViewModel>();
            List<PackageService> packageServices = _packageServicesBLL.GetPackageServicesByPackageId(this.SelectedPackage.Id);
            foreach (PackageService packageService in packageServices)
            {
                PatientRegistrationServiceViewModel prsvm = new PatientRegistrationServiceViewModel()
                {
                    PatientRegistrationService = new PatientRegistrationService()
                    {
                        PatientRegistrationId = this.PatientRegistration.Id,
                        ServiceId = packageService.ServiceId,
                        Price = packageService.Price,
                        IsActive = true
                    },
                    Service = _servicesBLL.GetService(packageService.ServiceId)
                };
                this.PatientRegistrationServices.Add(prsvm);
            }
        }
        #endregion

        #region Public Methods
        public ObservableCollection<PatientRegistrationServiceViewModel> PatientRegistrationServiceViewModelList(List<PatientRegistrationService> patientRegistrationServices)
        {
            List<PatientRegistrationServiceViewModel> packageServicesList = new List<PatientRegistrationServiceViewModel>();
            foreach (PatientRegistrationService patientRegistrationService in patientRegistrationServices)
                packageServicesList.Add(new PatientRegistrationServiceViewModel()
                {
                    PatientRegistrationService = patientRegistrationService,
                    Service = _servicesBLL.GetService(patientRegistrationService.ServiceId)
                });

            return new ObservableCollection<PatientRegistrationServiceViewModel>(packageServicesList);
        }

        public void RefreshComboBoxes()
        {
            this.SelectedCompany = this.Companies.FirstOrDefault();
            this.SelectedPackage = this.Packages.FirstOrDefault();
            this.SelectedBatchName = string.Empty;
        }

        public void LoadPatientRegistrationComboBoxes()
        {
            this.Companies = new ObservableCollection<Company>(_commonFunctions.CompaniesList(true));
            this.Packages = new ObservableCollection<Package>(_commonFunctions.PackagesList(true));

            RefreshComboBoxes();
            RefreshComboBoxesByCompanyId(true);
        }
        #endregion

        #region Private Methods
        private void ComputeTotalPrice()
        {
            if (!this.PatientRegistration.IsPriceEdited)
            {
                decimal price = this.PatientRegistrationServices.Select(p => p.PatientRegistrationService.Price).Sum();
                this.PatientRegistration.AmountDue = price;
                this.PatientRegistration.PatientRegistrationAmountDue = String.Format("{0:N}", price);
                this.PatientRegistration.IsPriceEdited = false;
            }
        }
        #endregion
    }
}
