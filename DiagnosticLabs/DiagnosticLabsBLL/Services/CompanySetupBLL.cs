using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class CompanySetupBLL
    {
        private const string _logFileName = "CompanySetupBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public CompanySetupBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public CompanySetup GetLatestCompanySetup()
        {
            try
            {
                return _dbContext.CompanySetups.OrderBy(i => i.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SaveCompanySetup(CompanySetup companySetup)
        {
            try
            {
                companySetup.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                companySetup.UpdatedDate = DateTime.Now;
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public List<LatestCodeNumber> GetLatestCodeNumbers()
        {
            try
            {
                return _dbContext.LatestCodeNumbers.ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

    }
}
