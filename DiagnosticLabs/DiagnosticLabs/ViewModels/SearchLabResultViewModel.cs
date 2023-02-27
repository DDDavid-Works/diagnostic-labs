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
    public class SearchLabResultViewModel : BaseViewModel
    {
        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();
        LabResultsBLL _labResultsBLL = new LabResultsBLL();

        #region Public Properties
        public string Service { get; set; }

        public ObservableCollection<LabResult> LabResults { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        private string _patientName;
        public string PatientName
        {
            get { return _patientName; }
            set { _patientName = value; OnPropertyChanged("PatientName"); SearchLabResultDetails(false); }
        }

        private DateTime? _dateRequested;
        public DateTime? DateRequested
        {
            get { return _dateRequested; }
            set { _dateRequested = value; OnPropertyChanged("DateRequested"); }
        }
        
        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }
        
        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchLabResultViewModel(string service)
        {
            this.Service = service;
            this.Companies = new ObservableCollection<Company>(_commonFunctions.CompaniesList(true, true));
            this.SelectedCompany = this.Companies.First();
            this.PatientName = string.Empty;
            this.DateRequested = DateTime.Today;

            this.SearchCommand = new RelayCommand(param => SearchLabResultDetails((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchLabResultDetails(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.PatientName.Trim() == string.Empty)) return;

            List<LabResult> labResults = _labResultsBLL.GetLabResults(this.Service, this.PatientName, this.SelectedCompany.Id, this.SelectedCompany.CompanyName, this.DateRequested);

            this.LabResults = new ObservableCollection<LabResult>(labResults);
        }
        #endregion
    }
}
