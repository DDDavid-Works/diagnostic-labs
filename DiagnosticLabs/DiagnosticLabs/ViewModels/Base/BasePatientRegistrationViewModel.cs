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
        CommonFunctions commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();
        PatientRegistrationServicesBLL patientRegistrationServicesBLL = new PatientRegistrationServicesBLL();
        PatientsBLL patientsBLL = new PatientsBLL();
        ServicesBLL servicesBLL = new ServicesBLL();
        PackagesBLL packagesBLL = new PackagesBLL();
        PackageServicesBLL packageServicesBLL = new PackageServicesBLL();

        #region Public Properties
        public PatientRegistration PatientRegistration { get; set; }
        public ObservableCollection<PatientRegistrationServiceViewModel> PatientRegistrationServices { get; set; }

        public ICommand AddPatientRegistrationServiceCommand { get; set; }
        public ICommand RemovePatientRegistrationServiceCommand { get; set; }
        public ICommand UpdatePatientRegistrationServiceCommand { get; set; }
        public ICommand UpdateIsPriceEditedCommand { get; set; }
        public ICommand RefreshPatientRegistrationBatchCommand { get; set; }
        public ICommand LoadPatientRegistrationCommand { get; set; }

        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<Package> Packages { get; set; }
        public ObservableCollection<PatientRegistrationBatch> PatientRegistrationBatches { get; set; }

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
                LoadPackageServices();
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
            LoadPatientRegistrationComboBoxes();

            this.AddPatientRegistrationServiceCommand = new RelayCommand(param => AddPatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.RemovePatientRegistrationServiceCommand = new RelayCommand(param => RemovePatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.UpdatePatientRegistrationServiceCommand = new RelayCommand(param => UpdatePatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.UpdateIsPriceEditedCommand = new RelayCommand(param => UpdateIsPriceEdited());
            this.RefreshPatientRegistrationBatchCommand = new RelayCommand(param => RefreshComboBoxesByCompanyId());
            this.LoadPatientRegistrationCommand = new RelayCommand(param => LoadPatientRegistration((long)param));
        }

        #region Data Actions
        public ObservableCollection<PatientRegistrationServiceViewModel> PatientRegistrationServiceViewModelList(List<PatientRegistrationService> patientRegistrationServices)
        {
            List<PatientRegistrationServiceViewModel> packageServicesList = new List<PatientRegistrationServiceViewModel>();
            foreach (PatientRegistrationService patientRegistrationService in patientRegistrationServices)
                packageServicesList.Add(new PatientRegistrationServiceViewModel()
                {
                    PatientRegistrationService = patientRegistrationService,
                    Service = servicesBLL.GetService(patientRegistrationService.ServiceId)
                });

            return new ObservableCollection<PatientRegistrationServiceViewModel>(packageServicesList);
        }

        public void LoadPatientRegistration(long id)
        {
            this.PatientRegistration = patientRegistrationsBLL.GetPatientRegistration(id);
            this.PatientRegistration.IsPriceEdited = true;
            this.Patient = patientsBLL.GetPatient((long)PatientRegistration.PatientId);
            this.Patient.IsAgeEdited = true;

            this.SelectedCompany = this.Companies.Where(c => c.Id == (this.PatientRegistration.CompanyId == null ? 0 : this.PatientRegistration.CompanyId)).FirstOrDefault();
            this.SelectedPackage = this.Packages.Where(p => p.Id == (this.PatientRegistration.PackageId == null ? 0 : this.PatientRegistration.PackageId)).FirstOrDefault();
            this.SelectedBatchName = this.PatientRegistration.BatchName;

            this.PatientRegistrationServices = PatientRegistrationServiceViewModelList(patientRegistrationServicesBLL.GetPatientRegistrationServicesByPatientRegistrationId(id));
        }

        public void RefreshComboBoxes()
        {
            this.SelectedCompany = this.Companies.FirstOrDefault();
            this.SelectedPackage = this.Packages.FirstOrDefault();
            this.SelectedBatchName = string.Empty;
        }

        public void LoadPatientRegistrationComboBoxes()
        {
            this.Companies = new ObservableCollection<Company>(commonFunctions.CompaniesList(true));
            this.Packages = new ObservableCollection<Package>(commonFunctions.PackagesList(true));

            RefreshComboBoxes();

            RefreshComboBoxesByCompanyId();
        }

        public virtual void UpdateIsPriceEdited()
        {
            this.PatientRegistration.IsPriceEdited = true;
        }
        #endregion

        #region Private Methods
        private void LoadPackageServices()
        {
            if (this.SelectedPackage == null) return;

            this.PatientRegistrationServices = new ObservableCollection<PatientRegistrationServiceViewModel>();

            List<PackageService> packageServices = packageServicesBLL.GetPackageServicesByPackageId(this.SelectedPackage.Id);
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
                    Service = servicesBLL.GetService(packageService.ServiceId)
                };
                this.PatientRegistrationServices.Add(prsvm);
            }
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

        private void ComputeTotalPrice()
        {
            if (!this.PatientRegistration.IsPriceEdited)
            {
                decimal price = this.PatientRegistrationServices.Select(p => p.PatientRegistrationService.Price).Sum();
                this.PatientRegistration.Price = price;
                this.PatientRegistration.PatientRegistrationPrice = String.Format("{0:0,0.00}", price);
                this.PatientRegistration.IsPriceEdited = false;
            }
        }

        private void RefreshComboBoxesByCompanyId()
        {
            long? companyId = this.SelectedCompany?.Id;
            this.PatientRegistrationBatches = new ObservableCollection<PatientRegistrationBatch>(commonFunctions.PatientRegistrationBatchList(companyId));
            this.Packages = new ObservableCollection<Package>(packagesBLL.GetPackagesByCompanyId(companyId));
        }
        #endregion
    }
}
