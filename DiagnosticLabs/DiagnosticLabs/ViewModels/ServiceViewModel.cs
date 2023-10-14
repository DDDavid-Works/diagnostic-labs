using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;
using System;

namespace DiagnosticLabs.ViewModels
{
    public class ServiceViewModel : BaseViewModel
    {
        private const string _entityName = "Service";

        CommonFunctions _commonFunctions = new CommonFunctions();
        ServicesBLL _servicesBLL = new ServicesBLL();

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
                this.Service = _servicesBLL.GetService(id);
                this.Service.ServicePrice = String.Format("{0:N}", this.Service.Price);
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
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.Service.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.Service.Id;
            if (_servicesBLL.SaveService(this.Service, ref id))
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

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Service.Id;
            this.Service.IsActive = false;
            if (_servicesBLL.SaveService(this.Service, ref id))
            {
                this.Service = _servicesBLL.GetLatestService();
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
