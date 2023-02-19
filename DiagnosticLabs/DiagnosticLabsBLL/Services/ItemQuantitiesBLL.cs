using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class ItemQuantitiesBLL
    {
        private const string _logFileName = "ItemQuantitiesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public ItemQuantitiesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public ItemQuantity GetItemQuantity(long id)
        {
            try
            {
                return _dbContext.ItemQuantities.Where(i => i.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<ItemQuantity> GetItemQuantityDetailsByItemId(long itemId)
        {
            try
            {
                return _dbContext.ItemQuantities.Where(i => i.ItemId == itemId && i.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
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
                    _dbContext.ItemQuantities.Add(itemQuantity);
                }
                else
                {
                    itemQuantity.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    itemQuantity.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = itemQuantity.Id;

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
