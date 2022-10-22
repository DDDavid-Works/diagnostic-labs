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
        CommonFunctions commonFunctions = new CommonFunctions();
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Public Properties
        public ObservableCollection<PatientRegistrationDetail> PatientRegistrationDetails { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        private string _PatientName;
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; OnPropertyChanged("PatientName"); SearchPatientRegistrationDetails(false); }
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
            this.Companies = new ObservableCollection<Company>(commonFunctions.CompaniesList(true, true));
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

            List<PatientRegistrationDetail> patientRegistrationDetails = patientRegistrationsBLL.GetPatientRegistrationDetails(this.PatientName, this.SelectedCompany.Id, this.InputDate);

            this.PatientRegistrationDetails = new ObservableCollection<PatientRegistrationDetail>(patientRegistrationDetails);
        }
        #endregion
    }
}
