using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticLabsBLL.Services
{
    public class PackagesBLL
    {
        private const string LogFileName = "PackagesBLL";

        CommonFunctions commonFunctions = new CommonFunctions();
        PackageServicesBLL packageServicesBLL = new PackageServicesBLL();

        private static DatabaseContext dbContext;

        public PackagesBLL()
        {
            dbContext = new DatabaseContext();
        }

        public Package GetPackage(long id)
        {
            try
            {
                return dbContext.Packages.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public Package GetLatestPackage()
        {
            try
            {
                return dbContext.Packages.Where(p => p.IsActive).OrderBy(p => p.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Package> GetAllPackages()
        {
            try
            {
                return dbContext.Packages.Where(p => p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Package> GetPackages(string name, long? companyId)
        {
            try
            {
                return dbContext.Packages.Where(p => (name == string.Empty || p.PackageName.ToUpper().Contains(name.ToUpper())) &&
                                                     (companyId == -1 || p.CompanyId == companyId) &&
                                                     p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SavePackage(Package package, ref long id)
        {
            try
            {
                package.Price = PackagePrice(package.Price, package.PackagePrice);
                if (package.Id == 0)
                {
                    package.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    package.CreatedDate = DateTime.Now;
                    package.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    package.UpdatedDate = DateTime.Now;
                    dbContext.Packages.Add(package);
                }
                else
                {
                    package.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    package.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = package.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }

        public bool SaveWithPackageServices(Package service, List<PackageService> packageServices, ref long id)
        {
            try
            {
                if (SavePackage(service, ref id))
                    return packageServicesBLL.SavePackageServiceList(packageServices, id);
                else
                    return false;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }

        #region Private Methods
        private decimal PackagePrice(decimal packagePrice, string packagePriceString)
        {
            decimal newPackagePrice = 0;
            bool isDecimal = decimal.TryParse(packagePriceString, out newPackagePrice);
            if (isDecimal && newPackagePrice != packagePrice)
                return newPackagePrice;
            else
                return packagePrice;
        }
        #endregion
    }
}
