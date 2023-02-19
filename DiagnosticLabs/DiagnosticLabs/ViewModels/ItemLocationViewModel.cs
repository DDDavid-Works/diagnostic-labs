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
        private const string _entityName = "Item Location";

        CommonFunctions _commonFunctions = new CommonFunctions();
        ItemLocationsBLL _itemLocationsBLL = new ItemLocationsBLL();

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
                this.ItemLocation = _itemLocationsBLL.GetItemLocation(id);

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
                this.NotificationMessage = _commonFunctions.CustomNotificationMessage(this.ItemLocation.ErrorMessages, Messages.MessageType.Error, false);
                return;
            }

            long id = this.ItemLocation.Id;
            if (_itemLocationsBLL.SaveItemLocations(this.ItemLocation, ref id))
            {
                this.ItemLocation.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeleteItemLocation()
        {
            if (this.ItemLocation.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.ItemLocation.Id;
            this.ItemLocation.IsActive = false;
            if (_itemLocationsBLL.SaveItemLocations(this.ItemLocation, ref id))
            {
                this.ItemLocation = _itemLocationsBLL.GetLatestItemLocation();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }
        #endregion
    }
}
