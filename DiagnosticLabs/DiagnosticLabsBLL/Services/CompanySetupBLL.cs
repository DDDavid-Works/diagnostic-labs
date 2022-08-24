using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticLabsBLL.Services
{
    public class CompanySetupBLL
    {
        private const string LogFileName = "CompanySetupBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public CompanySetupBLL()
        {
            dbContext = new DatabaseContext();
        }

        public CompanySetup GetLatestCompanySetup()
        {
            try
            {
                return dbContext.CompanySetups.OrderBy(i => i.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SaveCompanySetup(CompanySetup companySetup)
        {
            try
            {
                companySetup.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                companySetup.UpdatedDate = DateTime.Now;
                dbContext.SaveChanges();

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
