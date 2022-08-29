using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class ServiceViewModel : BaseViewModel
    {
        private const string EntityName = "Service";

        ServicesBLL servicesBLL = new ServicesBLL();

        #region Public Properties
        public Service Service { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public ServiceViewModel(long id)
        {
            if (id == 0)
                NewService();
            else
            {
                this.Service = servicesBLL.GetService(id);
                this.Service.ServicePrice = this.Service.Price.ToString("0.00");
            }

            this.NewCommand = new RelayCommand(param => NewService());
            this.SaveCommand = new RelayCommand(param => SaveService());
            this.DeleteCommand = new RelayCommand(param => DeleteService());
        }

        #region Data Actions
        private void NewService()
        {
            if (this.Service == null)
                this.Service = new Service();

            this.Service.Id = 0;
            this.Service.ServiceName = string.Empty;
            this.Service.ServiceDescription = string.Empty;
            this.Service.Price = 0;
            this.Service.IsActive = true;
            this.Service.ServicePrice = "0.00";
            this.ClearNotificationMessages();
        }

        private void SaveService()
        {
            if (!this.Service.IsValid)
            {
                this.NotificationMessages = this.Service.ErrorMessages;
                return;
            }

            long id = this.Service.Id;
            if (servicesBLL.SaveService(this.Service, ref id))
            {
                this.Service.Id = id;
                this.NotificationMessages = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.SaveFailed;
        }

        private void DeleteService()
        {
            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this service?", EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Service.Id;
            this.Service.IsActive = false;
            if (servicesBLL.SaveService(this.Service, ref id))
            {
                this.Service = servicesBLL.GetLatestService();
                this.NotificationMessages = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.DeleteFailed;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
