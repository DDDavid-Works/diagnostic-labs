using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchPackageViewModel : BaseViewModel
    {
        CommonFunctions commonFunctions = new CommonFunctions();
        PackagesBLL packagesBLL = new PackagesBLL();

        #region Public Properties
        public ObservableCollection<Package> Packages { get; set; }
        public ObservableCollection<Company> Companies { get; set; }

        private string _PackageName;
        public string PackageName
        {
            get { return _PackageName; }
            set { _PackageName = value; OnPropertyChanged("PackageName"); }
        }

        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchPackageViewModel()
        {
            this.Companies = new ObservableCollection<Company>(commonFunctions.CompaniesList(true, true));
            this.SelectedCompany = this.Companies.First();
            this.PackageName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchPackages());
        }

        #region Private Methods
        private void SearchPackages()
        {
            List<Package> packages = packagesBLL.GetPackages(this.PackageName, this.SelectedCompany.Id);
            this.Packages = new ObservableCollection<Package>(packages);
        }
        #endregion
    }
}
