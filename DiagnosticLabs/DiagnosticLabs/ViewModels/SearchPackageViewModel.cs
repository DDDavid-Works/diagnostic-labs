﻿using DiagnosticLabs.ViewModels.Base;
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
        CommonFunctions _commonFunctions = new CommonFunctions();
        PackagesBLL _packagesBLL = new PackagesBLL();

        #region Public Properties
        public ObservableCollection<Package> Packages { get; set; }
        public ObservableCollection<Company> Companies { get; set; }

        private string _packageName;
        public string PackageName
        {
            get { return _packageName; }
            set { _packageName = value; OnPropertyChanged("PackageName"); SearchPackages(false); }
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; OnPropertyChanged("SelectedCompany"); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchPackageViewModel()
        {
            this.Companies = new ObservableCollection<Company>(_commonFunctions.CompaniesList(true, true));
            this.SelectedCompany = this.Companies.First();
            this.PackageName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchPackages((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchPackages(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.PackageName.Trim() == string.Empty)) return;

            List<Package> packages = _packagesBLL.GetPackages(this.PackageName, this.SelectedCompany.Id);
            this.Packages = new ObservableCollection<Package>(packages);
        }
        #endregion
    }
}
