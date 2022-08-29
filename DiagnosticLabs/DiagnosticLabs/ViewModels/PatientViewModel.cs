using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabs.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class PatientViewModel : BaseViewModel
    {
        private const string EntityName = "Patient";

        DiagnosticLabsBLL.Services.CommonFunctions bllCommonFunctions = new DiagnosticLabsBLL.Services.CommonFunctions();
        CommonFunctions commonFunctions = new CommonFunctions();
        PatientsBLL patientsBLL = new PatientsBLL();
        CompaniesBLL companiesBLL = new CompaniesBLL();

        #region Public Properties
        public Patient Patient { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateAgeByDateOfBirthCommand { get; set; }
        public ICommand UpdateIsAgeEditedCommand { get; set; }
        public ICommand RefreshSingleLineEntryListCommand { get; set; }

        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<string> Genders { get; set; }
        public ObservableCollection<string> CivilStatuses { get; set; }

        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }
        #endregion

        public PatientViewModel(long id)
        {
            List<Company> companies = companiesBLL.GetAllCompanies();
            companies.Insert(0, new Company() { Id = 0, CompanyName = "Walk-in" });
            this.Companies = new ObservableCollection<Company>(companies);
            LoadAllSingleLineEntryLists();

            if (id == 0)
                NewPatient();
            else
            {
                this.Patient = patientsBLL.GetPatient(id);
                this.Patient.IsAgeEdited = true;
                long companyId = this.Patient.CompanyId == null ? 0 : this.Patient.CompanyId.Value;
                this.SelectedCompany = this.Companies.Where(c => c.Id == companyId).FirstOrDefault();
            }

            this.NewCommand = new RelayCommand(param => NewPatient());
            this.SaveCommand = new RelayCommand(param => SavePatient());
            this.DeleteCommand = new RelayCommand(param => DeletePatient());
            this.UpdateAgeByDateOfBirthCommand = new RelayCommand(param => UpdateAgeByDateOfBirth((DateTime?)param));
            this.UpdateIsAgeEditedCommand = new RelayCommand(param => UpdateIsAgeEdited());
            this.RefreshSingleLineEntryListCommand = new RelayCommand(param => RefreshSingleLineEntryList((string)param));
        }

        #region Data Actions
        private void NewPatient()
        {
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
            this.SelectedCompany = this.Companies.First();
            this.ClearNotificationMessages();
        }

        private void SavePatient()
        {
            this.Patient.CompanyId = this.SelectedCompany.Id;
            if (!this.Patient.IsValid)
            {
                this.NotificationMessages = this.Patient.ErrorMessages;
                return;
            }

            long id = this.Patient.Id;
            if (patientsBLL.SavePatient(this.Patient, ref id))
            {
                this.Patient.Id = id;
                this.NotificationMessages = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.SaveFailed;
        }

        private void DeletePatient()
        {
            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Patient.Id;
            this.Patient.IsActive = false;
            if (patientsBLL.SavePatient(this.Patient, ref id))
            {
                this.Patient = patientsBLL.GetLatestPatient();
                this.NotificationMessages = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.DeleteFailed;
        }
        #endregion

        #region Private Methods
        private void LoadAllSingleLineEntryLists()
        {
            RefreshSingleLineEntryList(SingleLineEntries.Gender);
            RefreshSingleLineEntryList(SingleLineEntries.CivilStatus);
        }

        private void UpdateAgeByDateOfBirth(DateTime? dateOfBirth)
        {
            if (!this.Patient.IsAgeEdited)
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
