using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class SearchDepartmentViewModel : BaseViewModel
    {
        DepartmentsBLL _departmentsBLL = new DepartmentsBLL();

        #region Public Properties
        public ObservableCollection<Department> Departments { get; set; }

        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set { _departmentName = value; OnPropertyChanged("DepartmentName"); SearchDepartments(false); }
        }

        public ICommand SearchCommand { get; set; }
        #endregion

        public SearchDepartmentViewModel()
        {
            this.DepartmentName = string.Empty;

            this.SearchCommand = new RelayCommand(param => SearchDepartments((bool)param));

            this.Init = false;
        }

        #region Private Methods
        private void SearchDepartments(bool isBlankSearch)
        {
            if (this.Init || (!isBlankSearch && this.DepartmentName.Trim() == string.Empty)) return;

            List<Department> departments = _departmentsBLL.GetDepartments(this.DepartmentName);
            this.Departments = new ObservableCollection<Department>(departments);
        }
        #endregion
    }
}
