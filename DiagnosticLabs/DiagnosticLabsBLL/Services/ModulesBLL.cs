using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class ModulesBLL
    {
        private const string LogFileName = "ModulesBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public ModulesBLL()
        {
            dbContext = new DatabaseContext();
        }

        public Module GetModule(long id)
        {
            try
            {
                return dbContext.Modules.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<Module> GetModules()
        {
            try
            {
                return dbContext.Modules.Where(m => m.IsActive).OrderBy(m => m.SortOrder).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }
    }
}
