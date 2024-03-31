using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class PackageServicesBLL
    {
        private const string _logFileName = "PackageServicesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public PackageServicesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public List<PackageService> GetPackageServicesByPackageId(long packageId)
        {
            try
            {
                return _dbContext.PackageServices.Where(p => p.PackageId == packageId && p.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SavePackageService(PackageService packageService, ref long id)
        {
            try
            {
                if (packageService.Id == 0)
                {
                    packageService.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    packageService.CreatedDate = DateTime.Now;
                    packageService.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    packageService.UpdatedDate = DateTime.Now;
                    _dbContext.PackageServices.Add(packageService);
                }
                else
                {
                    packageService.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    packageService.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = packageService.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SavePackageServiceList(List<PackageService> packageServices, long packageId)
        {
            try
            {
                List<PackageService> existingPackageServices = GetPackageServicesByPackageId(packageId);
                List<long> existingPackageServicesIds = existingPackageServices.Select(p => p.Id).ToList();
                List<PackageService> packageServicesToRemove = existingPackageServices.Where(p => !packageServices.Select(ps => ps.Id).Contains(p.Id) && p.Id != 0).ToList();
                foreach (PackageService packageService in packageServicesToRemove)
                {
                    long packageServiceId = 0;
                    packageService.IsActive = false;
                    SavePackageService(packageService, ref packageServiceId);
                }

                foreach (PackageService packageService in packageServices)
                {
                    if (packageService.PackageId == 0)
                        packageService.PackageId = packageId;

                    long packageServiceId = 0;
                    packageService.Price = Convert.ToDecimal(_commonFunctions.NumbericValue(packageService.PackageServicePrice));

                    SavePackageService(packageService, ref packageServiceId);
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
