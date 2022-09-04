using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchPatientRegistrationViewModel : BaseViewModel
    {
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();
        CompaniesBLL companiesBLL = new CompaniesBLL();

        #region Public Properties
        public ObservableCollection<PatientRegistrationDetail> PatientRegistrationDetails { get; set; }

        public ObservableCollection<Company> Companies { get; set; }
        
        private string _PatientName;
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; OnPropertyChanged("PatientName"); }
        }

        private DateTime? _InputDate;
        public DateTime? InputDate
        {
            get { return _InputDate; }
            set { _InputDate = value; OnPropertyChanged("InputDate"); }
        }
        
        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }
        
        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchPatientRegistrationViewModel()
        {
            List<Company> companies = companiesBLL.GetAllCompanies();
            companies.Insert(0, new Company() { Id = 0, CompanyName = "ALL", Address = "ALL", ContactNumbers = "ALL", ContactPerson = "ALL", IsActive = true });
            this.Companies = new ObservableCollection<Company>(companies);

            this.PatientName = string.Empty;
            this.InputDate = DateTime.Today;

            this.SearchCommand = new RelayCommand(param => SearchPatientRegistrationDetails());
        }

        #region Private Methods
        private void SearchPatientRegistrationDetails()
        {
            long companyId = this.SelectedCompany != null ? this.SelectedCompany.Id : 0;
            List<PatientRegistrationDetail> patientRegistrationDetails = patientRegistrationsBLL.GetPatientRegistrationDetails(this.PatientName, companyId, this.InputDate);
            this.PatientRegistrationDetails = new ObservableCollection<PatientRegistrationDetail>(patientRegistrationDetails);
        }
        #endregion
    }
}
