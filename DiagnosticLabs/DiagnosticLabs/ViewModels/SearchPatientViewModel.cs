using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchPatientViewModel : BaseViewModel
    {
        CommonFunctions commonFunctions = new CommonFunctions();
        PatientsBLL patientsBLL = new PatientsBLL();

        #region Public Properties
        public ObservableCollection<PatientCompany> PatientCompanies { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        private string _PatientName;
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; OnPropertyChanged("PatientName"); SearchPatientCompanies(false); }
        }

        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchPatientViewModel()
        {
            this.Companies = new ObservableCollection<Company>(commonFunctions.CompaniesList(true, true));

            this.SelectedCompany = this.Companies.First();
            this.PatientName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchPatientCompanies((bool)param));

            this.Init = false;

        }

        #region Private Methods
        private void SearchPatientCompanies(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.PatientName.Trim() == string.Empty)) return;

            List<PatientCompany> patientCompanies = patientsBLL.GetPatientCompanies(this.PatientName, this.SelectedCompany.Id);

            this.PatientCompanies = new ObservableCollection<PatientCompany>(patientCompanies);
        }
        #endregion
    }
}
