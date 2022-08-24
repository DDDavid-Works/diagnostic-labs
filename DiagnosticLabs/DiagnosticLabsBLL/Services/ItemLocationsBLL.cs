using DiagnosticLabsBLL;
using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticLabsBLL.Services
{
    public class ItemLocationsBLL
    {
        private const string LogFileName = "ItemLocationsBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public ItemLocationsBLL()
        {
            dbContext = new DatabaseContext();
        }

        public ItemLocation GetItemLocation(long id)
        {
            try
            {
                return dbContext.ItemLocations.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public ItemLocation GetLatestItemLocation()
        {
            try
            {
                return dbContext.ItemLocations.Where(i => i.IsActive).OrderBy(i => i.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<ItemLocation> GetAllItemLocations()
        {
            try
            {
                return dbContext.ItemLocations.Where(i => i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<ItemLocation> GetItemLocations(string name)
        {
            try
            {
                return dbContext.ItemLocations.Where(i => (name == string.Empty || i.ItemLocationName.ToUpper().Contains(name.ToUpper())) && i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SaveItemLocations(ItemLocation itemLocation, ref long id)
        {
            try
            {
                if (itemLocation.Id == 0)
                {
                    itemLocation.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    itemLocation.CreatedDate = DateTime.Now;
                    itemLocation.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    itemLocation.UpdatedDate = DateTime.Now;
                    dbContext.ItemLocations.Add(itemLocation);
                }
                else
                {
                    itemLocation.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    itemLocation.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = itemLocation.Id;

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
