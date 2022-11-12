using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PatientRegistrationViewModel : BaseViewModel
    {
        private const string EntityName = "Patient Registration";

        DiagnosticLabsBLL.Services.CommonFunctions bllCommonFunctions = new DiagnosticLabsBLL.Services.CommonFunctions();
        CommonFunctions commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();
        PatientRegistrationServicesBLL patientRegistrationServicesBLL = new PatientRegistrationServicesBLL();
        PatientsBLL patientsBLL = new PatientsBLL();
        ServicesBLL servicesBLL = new ServicesBLL();
        PackageServicesBLL packageServicesBLL = new PackageServicesBLL();

        #region Public Properties
        public PatientRegistration PatientRegistration { get; set; }
        public Patient Patient { get; set; }
        public ObservableCollection<PatientRegistrationServiceViewModel> PatientRegistrationServices { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddPatientRegistrationServiceCommand { get; set; }
        public ICommand RemovePatientRegistrationServiceCommand { get; set; }
        public ICommand UpdatePatientRegistrationServiceCommand { get; set; }
        public ICommand UpdateAgeByDateOfBirthCommand { get; set; }
        public ICommand UpdateIsAgeEditedCommand { get; set; }
        public ICommand UpdateIsPriceEditedCommand { get; set; }
        public ICommand RefreshSingleLineEntryListCommand { get; set; }

        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<Package> Packages { get; set; }
        public ObservableCollection<PatientRegistrationBatch> PatientRegistrationBatches { get; set; }
        public ObservableCollection<string> Genders { get; set; }
        public ObservableCollection<string> CivilStatuses { get; set; }

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
            set { 
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

        public PatientRegistrationViewModel(long id)
        {
            LoadComboBoxes();

            if (id == 0)
                NewPatientRegistration();
            else
            {
                this.PatientRegistration = patientRegistrationsBLL.GetPatientRegistration(id);
                this.PatientRegistration.PatientRegistrationPrice = this.PatientRegistration.Price.ToString("0.00");
                this.PatientRegistration.IsPriceEdited = true;
                this.Patient = patientsBLL.GetPatient((long)PatientRegistration.PatientId);
                this.Patient.IsAgeEdited = true;

                this.SelectedCompany = this.Companies.Where(c => c.Id == (this.PatientRegistration.CompanyId == null ? 0 : this.PatientRegistration.CompanyId)).FirstOrDefault();
                this.SelectedPackage = this.Packages.Where(p => p.Id == (this.PatientRegistration.PackageId == null ? 0 : this.PatientRegistration.PackageId)).FirstOrDefault();
                this.SelectedBatchName = this.PatientRegistration.BatchName;

                this.PatientRegistrationServices = PatientRegistrationServiceViewModelList(patientRegistrationServicesBLL.GetPatientRegistrationServicesByPatientRegistrationId(id));
            }

            this.NewCommand = new RelayCommand(param => NewPatientRegistration());
            this.SaveCommand = new RelayCommand(param => SavePatientRegistration());
            this.DeleteCommand = new RelayCommand(param => DeletePatientRegistration());
            this.AddPatientRegistrationServiceCommand = new RelayCommand(param => AddPatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.RemovePatientRegistrationServiceCommand = new RelayCommand(param => RemovePatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.UpdatePatientRegistrationServiceCommand = new RelayCommand(param => UpdatePatientRegistrationService((PatientRegistrationServiceViewModel)param));
            this.UpdateAgeByDateOfBirthCommand = new RelayCommand(param => UpdateAgeByDateOfBirth((DateTime?)param));
            this.UpdateIsAgeEditedCommand = new RelayCommand(param => UpdateIsAgeEdited());
            this.UpdateIsPriceEditedCommand = new RelayCommand(param => UpdateIsPriceEdited());
            this.RefreshSingleLineEntryListCommand = new RelayCommand(param => RefreshSingleLineEntryList((string)param));
        }

        #region Data Actions
        private void NewPatientRegistration()
        {
            if (this.PatientRegistration == null)
                this.PatientRegistration = new PatientRegistration();

            this.PatientRegistration.PatientId = null;
            this.PatientRegistration.CompanyId = null;
            this.PatientRegistration.PackageId = null;
            this.PatientRegistration.Price = 0;
            this.PatientRegistration.PatientRegistrationPrice = "0.00";
            this.PatientRegistration.InputDate = DateTime.Now;
            this.PatientRegistration.IsPriceEdited = false;

            if (this.Patient == null)
                this.Patient = new Patient();

            this.Patient.Id = 0;
            this.Patient.PatientName = string.Empty;
            this.Patient.DateOfBirth = null;
            this.Patient.Age = null;
            this.Patient.Gender = string.Empty;
            this.Patient.CivilStatus = string.Empty;
            this.Patient.Address = string.Empty;
            this.Patient.ContactNumbers = string.Empty;
            this.Patient.IsActive = true;
            this.Patient.IsAgeEdited = false;

            this.PatientRegistrationServices = new ObservableCollection<PatientRegistrationServiceViewModel>();
            this.SelectedCompany = this.Companies.First();
            this.SelectedPackage = this.Packages.First();
            this.ClearNotificationMessages();
        }

        private void SavePatientRegistration()
        {
            if (this.SelectedPackage.Id == 0)
                this.PatientRegistration.PackageId = null;
            else
                this.PatientRegistration.PackageId = this.SelectedPackage.Id;

            this.PatientRegistration.CompanyId = this.SelectedCompany.Id;
            this.PatientRegistration.BatchName = this.SelectedBatchName;
            if (!this.PatientRegistration.IsValid || !this.Patient.IsValid || this.PatientRegistrationServices.Where(p => !p.PatientRegistrationService.IsValid).Any())
            {
                string errorMessages = string.Empty;
                errorMessages = this.PatientRegistration.ErrorMessages;
                errorMessages += this.Patient.ErrorMessages;
                errorMessages += string.Join("", this.PatientRegistrationServices.Where(p => !p.PatientRegistrationService.IsValid).Select(p => p.PatientRegistrationService.ErrorMessages).ToList());

                this.NotificationMessage = commonFunctions.CustomNotificationMessage(errorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.PatientRegistration.Id;
            if (patientRegistrationsBLL.SavePatientRegistrationWithPatientAndServices(this.PatientRegistration, this.Patient, this.PatientRegistrationServices.Select(p => p.PatientRegistrationService).ToList(), ref id))
            {
                this.PatientRegistration.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeletePatientRegistration()
        {
            if (this.PatientRegistration.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.PatientRegistration.Id;
            this.PatientRegistration.IsActive = false;
            if (patientRegistrationsBLL.SavePatientRegistration(this.PatientRegistration, ref id))
            {
                this.PatientRegistration = patientRegistrationsBLL.GetLatestPatientRegistration();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion

        #region Private Methods
        private ObservableCollection<PatientRegistrationServiceViewModel> PatientRegistrationServiceViewModelList(List<PatientRegistrationService> patientRegistrationServices)
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

        private void LoadPackageServices()
        {
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
                this.PatientRegistration.PatientRegistrationPrice = price.ToString("0.00");
                this.PatientRegistration.IsPriceEdited = false;
            }
        }

        private void LoadComboBoxes()
        {
            this.Companies = new ObservableCollection<Company>(commonFunctions.CompaniesList(true));
            this.Packages = new ObservableCollection<Package>(commonFunctions.PackagesList(true));

            long? companyId = this.SelectedCompany?.Id;
            this.PatientRegistrationBatches = new ObservableCollection<PatientRegistrationBatch>(commonFunctions.PatientRegistrationBatchList(companyId));

            LoadAllSingleLineEntryLists();
        }

        private void LoadAllSingleLineEntryLists()
        {
            RefreshSingleLineEntryList(SingleLineEntries.Gender);
            RefreshSingleLineEntryList(SingleLineEntries.CivilStatus);
        }

        private void UpdateAgeByDateOfBirth(DateTime? dateOfBirth)
        {
            if (!this.Patient.IsAgeEdited && dateOfBirth != null)
            {
                int age = bllCommonFunctions.ComputeAge((DateTime)dateOfBirth);
                this.Patient.Age = age.ToString() + " years old";
                this.Patient.IsAgeEdited = false;
            }
        }

        private void UpdateIsAgeEdited()
        {
            this.Patient.IsAgeEdited = true;
        }

        private void UpdateIsPriceEdited()
        {
            this.PatientRegistration.IsPriceEdited = true;
        }

        private void RefreshSingleLineEntryList(string listName)
        {
            switch (listName)
            {
                case SingleLineEntries.Gender:
                    this.Genders = new ObservableCollection<string>(commonFunctions.GeneralSingleLineEntryList(SingleLineEntries.Gender, true));
                    if (this.Patient != null && this.Patient.Gender != string.Empty)
                        this.Patient.Gender = this.Genders.First();
                    break;
                case SingleLineEntries.CivilStatus:
                    this.CivilStatuses = new ObservableCollection<string>(commonFunctions.GeneralSingleLineEntryList(SingleLineEntries.CivilStatus, true));
                    if (this.Patient != null && this.Patient.CivilStatus != string.Empty)
                        this.Patient.CivilStatus = this.CivilStatuses.First();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
