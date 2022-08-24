using DiagnosticLabsBLL;
using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticLabsBLL.Services
{
    public class ItemQuantitiesBLL
    {
        private const string LogFileName = "ItemQuantitiesBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public ItemQuantitiesBLL()
        {
            dbContext = new DatabaseContext();
        }

        public ItemQuantity GetItemQuantity(long id)
        {
            try
            {
                return dbContext.ItemQuantities.Where(i => i.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<ItemQuantity> GetItemQuantityDetailsByItemId(long itemId)
        {
            try
            {
                return dbContext.ItemQuantities.Where(i => i.ItemId == itemId && i.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool Save(ItemQuantity itemQuantity, ref long id)
        {
            try
            {
                if (itemQuantity.Id == 0)
                {
                    itemQuantity.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    itemQuantity.CreatedDate = DateTime.Now;
                    itemQuantity.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    itemQuantity.UpdatedDate = DateTime.Now;
                    dbContext.ItemQuantities.Add(itemQuantity);
                }
                else
                {
                    itemQuantity.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    itemQuantity.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = itemQuantity.Id;

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
