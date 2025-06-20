﻿using DiagnosticLabs.Constants;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.POCOs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private const string _entityName = "Item";

        CommonFunctions _commonFunctions = new CommonFunctions();
        ItemsBLL _itemsBLL = new ItemsBLL();
        ItemLocationsBLL _itemLocationsBLL = new ItemLocationsBLL();
        ItemQuantitiesBLL _itemQuantitiesBLL = new ItemQuantitiesBLL();

        #region Public Properties
        public Item Item { get; set; }
        public ItemDetail ItemDetail { get; set; }
        public ObservableCollection<ItemQuantity> ItemQuantities { get; set; }
        public ObservableCollection<ItemLocation> ItemLocations { get; set; }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddItemQuantityCommand { get; set; }
        public ICommand DeleteItemQuantityCommand { get; set; }
        #endregion

        public ItemViewModel(long id)
        {
            this.ItemQuantities = new ObservableCollection<ItemQuantity>();
            this.ItemQuantities.CollectionChanged += ItemQuantities_CollectionChanged;
            if (id == 0)
                NewItem();
            else
            {
                this.Item = _itemsBLL.GetItem(id);
                foreach (var itemQuantity in _itemQuantitiesBLL.GetItemQuantityDetailsByItemId(id))
                    this.ItemQuantities.Add(itemQuantity);
            }
            this.ItemDetail = new ItemDetail() { TotalQuantity = this.ItemQuantities.Sum(i => i.Quantity) };
            this.ItemLocations = new ObservableCollection<ItemLocation>(_itemLocationsBLL.GetAllItemLocations());

            this.NewCommand = new RelayCommand(param => NewItem());
            this.SaveCommand = new RelayCommand(param => SaveItem());
            this.DeleteCommand = new RelayCommand(param => DeleteItem());
            this.AddItemQuantityCommand = new RelayCommand(param => AddItemQuantity());
            this.DeleteItemQuantityCommand = new RelayCommand(param => DeleteItemQuantity((ItemQuantity)param));
        }

        #region Data Actions
        private void NewItem()
        {
            if (this.Item == null)
                this.Item = new Item();

            this.Item.Id = 0;
            this.Item.ItemName = string.Empty;
            this.Item.Cost = 0;
            this.Item.IsActive = true;
            this.ItemQuantities.Clear();
            this.ClearNotificationMessages();
        }

        private void SaveItem()
        {
            if (!Item.IsValid) return;

            long id = this.Item.Id;
            if (_itemsBLL.SaveWithQuantities(this.Item, new List<ItemQuantity>(this.ItemQuantities), ref id))
            {
                this.Item.Id = id;
                this.NotificationMessage = Messages.SavedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.SaveFailed;
        }

        private void DeleteItem()
        {
            if (this.Item.Id == 0)
            {
                this.NotificationMessage = Messages.NothingToDelete;
                return;
            }

            MessageBoxResult confirmation = MessageBox.Show(_commonFunctions.ConfirmDeleteQuestion(_entityName), _entityName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.No) return;

            long id = this.Item.Id;
            this.Item.IsActive = false;
            if (_itemsBLL.SaveItem(this.Item, ref id))
            {
                this.Item = _itemsBLL.GetLatestItem();
                this.NotificationMessage = Messages.DeletedSuccessfully;
            }
            else
                this.NotificationMessage = Messages.DeleteFailed;
        }

        private void AddItemQuantity()
        {
            ItemQuantities.Add(new ItemQuantity() { Id = 0, ItemId = this.Item.Id, ItemLocationId = this.ItemLocations.Select(i => i.Id).First(), Quantity = 0, IsActive = true });
            UpdateTotalQuantity();
        }

        private void DeleteItemQuantity(ItemQuantity itemQuantity)
        {
            ItemQuantities.Remove(itemQuantity);
            UpdateTotalQuantity();
        }
        #endregion

        #region Private Methods
        private void ItemQuantities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (ItemQuantity item in e.NewItems)
                    item.PropertyChanged += ItemQuantity_PropertyChanged;

            if (e.OldItems != null)
                foreach (ItemQuantity item in e.OldItems)
                    item.PropertyChanged -= ItemQuantity_PropertyChanged;
        }

        private void ItemQuantity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTotalQuantity();
        }

        private void UpdateTotalQuantity()
        {
            ItemDetail.TotalQuantity = this.ItemQuantities.Sum(i => i.Quantity);
        }
        #endregion
    }
}
