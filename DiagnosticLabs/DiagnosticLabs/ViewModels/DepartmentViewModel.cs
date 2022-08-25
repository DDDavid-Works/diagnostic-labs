using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class DepartmentViewModel : BaseViewModel
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
            this.ClearNotificationMessages();
        }

        private void SaveDepartment()
        {
            if (!this.Department.IsValid)
            {
                this.NotificationMessages = this.Department.ErrorMessages;
                return;
            }
            
            long id = this.Department.Id;
            if (departmentsBLL.SaveDepartment(this.Department, ref id))
            {
                this.Department.Id = id;
                this.NotificationMessages = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.SaveFailed;
        }

        private void DeleteDepartment()
        {
            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this department?", EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Department.Id;
            this.Department.IsActive = false;
            if (departmentsBLL.SaveDepartment(this.Department, ref id))
            {
                this.Department = departmentsBLL.GetLatestDepartment();
                this.NotificationMessages = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.DeleteFailed;
        }
        #endregion
    }
}
