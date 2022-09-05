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

        CommonFunctions commonFunctions = new CommonFunctions();
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
            this.Companies = new ObservableCollection<Company>(commonFunctions.CompaniesList(true, true));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
