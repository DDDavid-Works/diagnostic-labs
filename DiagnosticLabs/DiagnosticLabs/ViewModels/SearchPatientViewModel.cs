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
        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientsBLL _patientsBLL = new PatientsBLL();

        #region Public Properties
        public ObservableCollection<PatientCompany> PatientCompanies { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        private string _patientName;
        public string PatientName
        {
            get { return _patientName; }
            set { _patientName = value; OnPropertyChanged("PatientName"); SearchPatientCompanies(false); }
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchPatientViewModel()
        {
            this.Companies = new ObservableCollection<Company>(_commonFunctions.CompaniesList(true, true));

            this.SelectedCompany = this.Companies.First();
            this.PatientName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchPatientCompanies((bool)param));

            this.Init = false;

        }

        #region Private Methods
        private void SearchPatientCompanies(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.PatientName.Trim() == string.Empty)) return;

            List<PatientCompany> patientCompanies = _patientsBLL.GetPatientCompanies(this.PatientName, this.SelectedCompany.Id);

            this.PatientCompanies = new ObservableCollection<PatientCompany>(patientCompanies);
        }
        #endregion
    }
}
