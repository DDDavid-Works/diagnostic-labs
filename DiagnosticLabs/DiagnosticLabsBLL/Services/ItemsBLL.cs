using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class ItemsBLL
    {
        private const string _logFileName = "ItemsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public ItemsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public Item GetItem(long id)
        {
            try
            {
                return _dbContext.Items.Find(id);
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public Item GetLatestItem()
        {
            try
            {
                return _dbContext.Items.Where(i => i.IsActive).OrderBy(i => i.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Item> GetAllItems()
        {
            try
            {
                return _dbContext.Items.Where(i => i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Item> GetItems(string name)
        {
            try
            {
                return _dbContext.Items.Where(i => (name == string.Empty || i.ItemName.ToUpper().Contains(name.ToUpper())) && i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SaveItem(Item item, ref long id)
        {
            try
            {
                if (item.Id == 0)
                {
                    item.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    item.UpdatedDate = DateTime.Now;
                    _dbContext.Items.Add(item);
                }
                else
                {
                    item.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    item.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = item.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveWithQuantities(Item item, List<ItemQuantity> itemQuantities, ref long id)
        {
            try
            {
                if (item.Id == 0)
                {
                    item.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    item.UpdatedDate = DateTime.Now;
                    _dbContext.Items.Add(item);
                }
                else
                {
                    item.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    item.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = item.Id;

                ItemQuantitiesBLL _itemQuantitiesBLL = new ItemQuantitiesBLL();
                
                List<ItemQuantity> oldItemQuantities = _itemQuantitiesBLL.GetItemQuantityDetailsByItemId(id);
                List<ItemQuantity> itemQuantitiesToDelete = oldItemQuantities.Where(i => !itemQuantities.Select(iq => iq.Id).Contains(i.Id)).ToList();
                foreach (var itemQuantityToDelete in itemQuantitiesToDelete)
                {
                    long itemQuantityId = 0;
                    itemQuantityToDelete.IsActive = false;
                    _itemQuantitiesBLL.Save(itemQuantityToDelete, ref itemQuantityId);
                }

                foreach (var itemQuantity in itemQuantities)
                {
                    long itemQuantityId = 0;
                    if (itemQuantity.Id != 0)
                    {
                        ItemQuantity quantity = _itemQuantitiesBLL.GetItemQuantity(itemQuantity.Id);
                        quantity.ItemId = itemQuantity.ItemId;
                        quantity.ItemLocationId = itemQuantity.ItemLocationId;
                        quantity.Quantity = itemQuantity.Quantity;
                        _itemQuantitiesBLL.Save(quantity, ref itemQuantityId);
                    } 
                    else
                    {
                        itemQuantity.ItemId = id;
                        _itemQuantitiesBLL.Save(itemQuantity, ref itemQuantityId);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }
    }
}
