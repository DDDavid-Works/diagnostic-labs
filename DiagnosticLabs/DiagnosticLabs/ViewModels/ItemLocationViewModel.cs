using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class ItemLocationViewModel : BaseViewModel
    {
        private const string EntityName = "Item Location";

        CommonFunctions commonFunctions = new CommonFunctions();
        ItemLocationsBLL itemLocationsBLL = new ItemLocationsBLL();

        #region Public Properties
        public ItemLocation ItemLocation { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        public ItemLocationViewModel(long id)
        {
            if (id == 0)
                NewItemLocation();
            else
                this.ItemLocation = itemLocationsBLL.GetItemLocation(id);

            this.NewCommand = new RelayCommand(param => NewItemLocation());
            this.SaveCommand = new RelayCommand(param => SaveItemLocation());
            this.DeleteCommand = new RelayCommand(param => DeleteItemLocation());
        }

        #region Data Actions
        private void NewItemLocation()
        {
            if (this.ItemLocation == null)
                this.ItemLocation = new ItemLocation();

            this.ItemLocation.Id = 0;
            this.ItemLocation.ItemLocationName = string.Empty;
            this.ItemLocation.IsActive = true;
            this.ClearNotificationMessages();
        }

        private void SaveItemLocation()
        {
            if (!this.ItemLocation.IsValid)
            {
                this.NotificationMessages = this.ItemLocation.ErrorMessages;
                return;
            }

            long id = this.ItemLocation.Id;
            if (itemLocationsBLL.SaveItemLocations(this.ItemLocation, ref id))
            {
                this.ItemLocation.Id = id;
                this.NotificationMessages = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.SaveFailed;
        }

        private void DeleteItemLocation()
        {
            MessageBoxResult confirmation = MessageBox.Show(commonFunctions.ConfirmDeleteQuestion(EntityName), EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.ItemLocation.Id;
            this.ItemLocation.IsActive = false;
            if (itemLocationsBLL.SaveItemLocations(this.ItemLocation, ref id))
            {
                this.ItemLocation = itemLocationsBLL.GetLatestItemLocation();
                this.NotificationMessages = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessages = Messages.DeleteFailed;
        }
        #endregion
    }
}
