using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticLabsBLL.Services
{
    public class PackageServicesBLL
    {
        private const string LogFileName = "PackageServicesBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public PackageServicesBLL()
        {
            dbContext = new DatabaseContext();
        }

        public List<PackageService> GetPackageServicesByPackageId(long packacgeId)
        {
            try
            {
                return dbContext.PackageServices.Where(p => p.PackageId == packacgeId && p.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
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
                    dbContext.PackageServices.Add(packageService);
                }
                else
                {
                    packageService.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    packageService.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = packageService.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
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
                    SavePackageService(packageService, ref packageServiceId);
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
