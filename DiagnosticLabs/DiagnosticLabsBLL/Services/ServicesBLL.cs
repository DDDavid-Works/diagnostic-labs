using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticLabsBLL.Services
{
    public class ServicesBLL
    {
        private const string LogFileName = "ServicesBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public ServicesBLL()
        {
            dbContext = new DatabaseContext();
        }

        public Service GetService(long id)
        {
            try
            {
                return dbContext.Services.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public Service GetLatestService()
        {
            try
            {
                return dbContext.Services.Where(s => s.IsActive).OrderBy(s => s.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Service> GetAllServices()
        {
            try
            {
                return dbContext.Services.Where(i => i.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Service> GetServices(string name)
        {
            try
            {
                return dbContext.Services.Where(s => (name == string.Empty || s.ServiceName.ToUpper().Contains(name.ToUpper())) && s.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SaveService(Service service, ref long id)
        {
            try
            {
                service.Price = ServicePrice(service.Price, service.ServicePrice);
                if (service.Id == 0)
                {
                    service.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    service.CreatedDate = DateTime.Now;
                    service.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    service.UpdatedDate = DateTime.Now;
                    dbContext.Services.Add(service);
                }
                else
                {
                    service.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    service.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = service.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }

        #region
        private decimal ServicePrice(decimal servicePrice, string servicePriceString)
        {
            decimal newServicePrice = 0;
            bool isDecimal = decimal.TryParse(servicePriceString, out newServicePrice);
            if (isDecimal && newServicePrice != servicePrice)
                return newServicePrice;
            else
                return servicePrice;
        }
        #endregion
    }
}
