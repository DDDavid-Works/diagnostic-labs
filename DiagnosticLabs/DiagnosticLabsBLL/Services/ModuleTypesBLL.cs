using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class ModuleTypesBLL
    {
        private const string _logFileName = "ModuleTypesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public ModuleTypesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public ModuleType GetModuleType(long id)
        {
            try
            {
                return _dbContext.ModuleTypes.Find(id);
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<ModuleType> GetModuleTypes()
        {
            try
            {
                return _dbContext.ModuleTypes.Where(m => m.IsActive).OrderBy(m => m.SortOrder).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }
    }
}
