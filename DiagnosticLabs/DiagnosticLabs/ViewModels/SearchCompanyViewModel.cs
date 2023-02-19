using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchCompanyViewModel : BaseViewModel
    {
        CompaniesBLL _companiesBLL = new CompaniesBLL();

        #region Public Properties
        public ObservableCollection<Company> Companies { get; set; }

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; OnPropertyChanged("CompanyName"); SearchCompanies(false); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchCompanyViewModel()
        {
            this.CompanyName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchCompanies((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchCompanies(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.CompanyName.Trim() == string.Empty)) return;

            List<Company> companies = _companiesBLL.GetCompanies(this.CompanyName);
            this.Companies = new ObservableCollection<Company>(companies);
        }
        #endregion
    }
}
