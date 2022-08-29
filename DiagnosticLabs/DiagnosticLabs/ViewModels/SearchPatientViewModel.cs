using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DiagnosticLabs.ViewModels
{
    public class SearchPatientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        CompaniesBLL companiesBLL = new CompaniesBLL();

        #region Public Properties
        public ObservableCollection<Company> Companies { get; set; }

        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }
        #endregion

        public SearchPatientViewModel()
        {
            List<Company> companies = companiesBLL.GetAllCompanies();
            companies.Insert(0, new Company() { Id = 0, CompanyName = "ALL", Address = "ALL", ContactNumbers = "ALL", ContactPerson = "ALL", IsActive = true });
            this.Companies = new ObservableCollection<Company>(companies);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
