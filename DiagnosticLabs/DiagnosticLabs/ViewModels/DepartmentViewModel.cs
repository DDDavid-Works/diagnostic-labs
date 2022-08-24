using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class DepartmentViewModel
    {
        private const string EntityName = "Department";

        DepartmentsBLL departmentsBLL = new DepartmentsBLL();

        #region Public Properties
        public Department Department { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public DepartmentViewModel(long id)
        {
            if (id == 0)
                this.Department = new Department() { Id = 0, DepartmentName = string.Empty, IsActive = true };
            else
                this.Department = departmentsBLL.GetDepartment(id);

            this.NewCommand = new RelayCommand(param => NewDepartment());
            this.SaveCommand = new RelayCommand(param => SaveDepartment());
            this.DeleteCommand = new RelayCommand(param => DeleteDepartment());
        }

        #region Data Actions
        private void NewDepartment()
        {
            this.Department.Id = 0;
            this.Department.DepartmentName = string.Empty;
            this.Department.DepartmentDescription = string.Empty;
            this.Department.IsActive = true;
        }

        private void SaveDepartment()
        {
            if (!this.Department.IsValid)
            {
                MessageBox.Show(this.Department.ErrorMessages, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            long id = this.Department.Id;
            if (departmentsBLL.SaveDepartment(this.Department, ref id))
            {
                this.Department.Id = id;
                MessageBox.Show(Messages.SavedSuccessfully, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(Messages.SaveFailed, EntityName, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DeleteDepartment()
        {
            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this department?", EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Department.Id;
            this.Department.IsActive = false;
            if (departmentsBLL.SaveDepartment(this.Department, ref id))
            {
                NewDepartment();
                MessageBox.Show(Messages.DeletedSuccessfully, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(Messages.DeleteFailed, EntityName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
    }
}
