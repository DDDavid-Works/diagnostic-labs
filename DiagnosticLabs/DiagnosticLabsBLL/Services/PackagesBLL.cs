using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class PackagesBLL
    {
        private const string _logFileName = "PackagesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();
        PackageServicesBLL _packageServicesBLL = new PackageServicesBLL();

        private static DatabaseContext _dbContext;

        public PackagesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public Package NewPackage()
        {
            return new Package()
            {
                Id = 0,
                PackageName = string.Empty,
                PackageDescription = string.Empty,
                Price = 0,
                IsActive = true,
                PackagePrice = "0.00",
                IsPriceEdited = false
            };
        }

        public Package GetPackage(long id)
        {
            try
            {
                Package package = _dbContext.Packages.Find(id);

                if (package != null)
                {
                    package.PackagePrice = String.Format("{0:N}", package.Price);
                    package.IsPriceEdited = true;
                }

                return package;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Package> GetPackagesByCompanyId(long? companyId, bool addNone = false)
        {
            try
            {
                List<Package> packages = _dbContext.Packages.Where(p => p.CompanyId == companyId && p.IsActive).ToList();

                if (addNone)
                    packages.Insert(0, new Package() { Id = 0, PackageName = "None" });

                return packages;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public Package GetLatestPackage()
        {
            try
            {
                return _dbContext.Packages.Where(p => p.IsActive).OrderBy(p => p.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Package> GetAllPackages()
        {
            try
            {
                return _dbContext.Packages.Where(p => p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Package> GetPackages(string name, long? companyId)
        {
            try
            {
                return _dbContext.Packages.Where(p => (name == string.Empty || p.PackageName.ToUpper().Contains(name.ToUpper())) &&
                                                     (companyId == -1 || p.CompanyId == companyId) &&
                                                     p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
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
                    _dbContext.Packages.Add(package);
                }
                else
                {
                    package.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    package.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = package.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveWithPackageServices(Package service, List<PackageService> packageServices, ref long id)
        {
            try
            {
                if (SavePackage(service, ref id))
                    return _packageServicesBLL.SavePackageServiceList(packageServices, id);
                else
                    return false;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
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
