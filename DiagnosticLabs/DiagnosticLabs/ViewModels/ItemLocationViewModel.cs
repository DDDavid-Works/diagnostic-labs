using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Constants;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class ItemLocationViewModel
    {
        private const string EntityName = "Item Location";

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
                this.ItemLocation = new ItemLocation() { Id = 0, ItemLocationName = string.Empty, IsActive = true };
            else
                this.ItemLocation = itemLocationsBLL.GetItemLocation(id);

            this.NewCommand = new RelayCommand(param => NewItemLocation());
            this.SaveCommand = new RelayCommand(param => SaveItemLocation());
            this.DeleteCommand = new RelayCommand(param => DeleteItemLocation());
        }

        #region Data Actions
        private void NewItemLocation()
        {
            this.ItemLocation.Id = 0;
            this.ItemLocation.ItemLocationName = string.Empty;
            this.ItemLocation.IsActive = true;
        }

        private void SaveItemLocation()
        {
            if (!this.ItemLocation.IsValid)
            {
                MessageBox.Show(this.ItemLocation.ErrorMessages, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            long id = this.ItemLocation.Id;
            if (itemLocationsBLL.SaveItemLocations(this.ItemLocation, ref id))
            {
                this.ItemLocation.Id = id;
                MessageBox.Show(Messages.SavedSuccessfully, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(Messages.SaveFailed, EntityName, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DeleteItemLocation()
        {
            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this item location?", EntityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.ItemLocation.Id;
            this.ItemLocation.IsActive = false;
            if (itemLocationsBLL.SaveItemLocations(this.ItemLocation, ref id))
            {
                this.ItemLocation = itemLocationsBLL.GetLatestItemLocation();
                MessageBox.Show(Messages.DeletedSuccessfully, EntityName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(Messages.DeleteFailed, EntityName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
    }
}
