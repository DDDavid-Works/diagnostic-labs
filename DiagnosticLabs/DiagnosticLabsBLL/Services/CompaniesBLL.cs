using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class CompaniesBLL
    {
        private const string LogFileName = "CompaniesBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public CompaniesBLL()
        {
            dbContext = new DatabaseContext();
        }

        public Company GetCompany(long id)
        {
            try
            {
                return dbContext.Companies.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public Company GetLatestCompany()
        {
            try
            {
                return dbContext.Companies.Where(c => c.IsActive && !c.IsSystem).OrderBy(i => i.Id).LastOrDefault();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Company> GetAllCompanies(bool includeSystemRecord = false)
        {
            try
            {
                return dbContext.Companies.Where(c => c.IsActive && (includeSystemRecord ? true : !c.IsSystem)).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Company> GetCompanies(string name)
        {
            try
            {
                return dbContext.Companies.Where(c => (name == string.Empty || c.CompanyName.ToUpper().Contains(name.ToUpper())) && c.IsActive && !c.IsSystem).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SaveCompany(Company company, ref long id)
        {
            try
            {
                if (company.Id == 0)
                {
                    company.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    company.CreatedDate = DateTime.Now;
                    company.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    company.UpdatedDate = DateTime.Now;
                    dbContext.Companies.Add(company);
                }
                else
                {
                    company.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    company.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = company.Id;

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
