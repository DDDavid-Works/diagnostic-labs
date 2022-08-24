using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class ModuleTypesBLL
    {
        private const string LogFileName = "ModuleTypesBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public ModuleTypesBLL()
        {
            dbContext = new DatabaseContext();
        }

        public ModuleType GetModuleType(long id)
        {
            try
            {
                return dbContext.ModuleTypes.Find(id);
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public List<ModuleType> GetModuleTypes()
        {
            try
            {
                return dbContext.ModuleTypes.Where(m => m.IsActive).OrderBy(m => m.SortOrder).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }
    }
}
