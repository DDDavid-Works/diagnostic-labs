using DiagnosticLabsBLL;
using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticLabsBLL.Services
{
    public class ItemsBLL
    {
        private const string LogFileName = "ItemsBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public ItemsBLL()
        {
            dbContext = new DatabaseContext();
        }

        public Item GetItem(long id)
        {
            try
            {
                return dbContext.Items.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public Item GetLatestItem()
        {
            try
            {
                return dbContext.Items.Where(i => i.IsActive).OrderBy(i => i.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Item> GetAllItems()
        {
            try
            {
                return dbContext.Items.Where(i => i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Item> GetItems(string name)
        {
            try
            {
                return dbContext.Items.Where(i => (name == string.Empty || i.ItemName.ToUpper().Contains(name.ToUpper())) && i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
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
                    dbContext.Items.Add(item);
                }
                else
                {
                    item.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    item.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = item.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
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
                    dbContext.Items.Add(item);
                }
                else
                {
                    item.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    item.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = item.Id;

                ItemQuantitiesBLL itemQuantitiesBLL = new ItemQuantitiesBLL();
                
                List<ItemQuantity> oldItemQuantities = itemQuantitiesBLL.GetItemQuantityDetailsByItemId(id);
                List<ItemQuantity> itemQuantitiesToDelete = oldItemQuantities.Where(i => !itemQuantities.Select(iq => iq.Id).Contains(i.Id)).ToList();
                foreach (var itemQuantityToDelete in itemQuantitiesToDelete)
                {
                    long itemQuantityId = 0;
                    itemQuantityToDelete.IsActive = false;
                    itemQuantitiesBLL.Save(itemQuantityToDelete, ref itemQuantityId);
                }

                foreach (var itemQuantity in itemQuantities)
                {
                    long itemQuantityId = 0;
                    if (itemQuantity.Id != 0)
                    {
                        ItemQuantity quantity = itemQuantitiesBLL.GetItemQuantity(itemQuantity.Id);
                        quantity.ItemId = itemQuantity.ItemId;
                        quantity.ItemLocationId = itemQuantity.ItemLocationId;
                        quantity.Quantity = itemQuantity.Quantity;
                        itemQuantitiesBLL.Save(quantity, ref itemQuantityId);
                    } 
                    else
                    {
                        itemQuantity.ItemId = id;
                        itemQuantitiesBLL.Save(itemQuantity, ref itemQuantityId);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }
    }
}
