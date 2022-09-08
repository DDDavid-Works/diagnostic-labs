using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class ServiceViewModel : BaseViewModel
    {
        private const string EntityName = "Service";

        CommonFunctions commonFunctions = new CommonFunctions();
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
                this.NotificationMessage = commonFunctions.CustomNotificationMessage(this.Service.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Service.Id;
            if (servicesBLL.SaveService(this.Service, ref id))
            {
                this.Service.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeleteService()
        {
            if (this.Service.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Service.Id;
            this.Service.IsActive = false;
            if (servicesBLL.SaveService(this.Service, ref id))
            {
                this.Service = servicesBLL.GetLatestService();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
