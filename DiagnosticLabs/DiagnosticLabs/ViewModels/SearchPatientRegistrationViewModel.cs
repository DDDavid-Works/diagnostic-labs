using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchPatientRegistrationViewModel : BaseViewModel
    {
        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public ObservableCollection<PatientRegistrationDetail> PatientRegistrationDetails { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        private string _patientName;
        public string PatientName
        {
            get { return _patientName; }
            set { _patientName = value; OnPropertyChanged("PatientName"); SearchPatientRegistrationDetails(false); }
        }

        private DateTime? _inputDate;
        public DateTime? InputDate
        {
            get { return _inputDate; }
            set { _inputDate = value; OnPropertyChanged("InputDate"); }
        }
        
        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }
        
        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchPatientRegistrationViewModel()
        {
            this.Companies = new ObservableCollection<Company>(_commonFunctions.CompaniesList(true, true));
            this.SelectedCompany = this.Companies.First();
            this.PatientName = string.Empty;
            this.InputDate = DateTime.Today;

            this.SearchCommand = new RelayCommand(param => SearchPatientRegistrationDetails((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchPatientRegistrationDetails(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.PatientName.Trim() == string.Empty)) return;

            List<PatientRegistrationDetail> patientRegistrationDetails = _patientRegistrationsBLL.GetPatientRegistrationDetails(this.PatientName, this.SelectedCompany.Id, this.InputDate);

            this.PatientRegistrationDetails = new ObservableCollection<PatientRegistrationDetail>(patientRegistrationDetails);
        }
        #endregion
    }
}
