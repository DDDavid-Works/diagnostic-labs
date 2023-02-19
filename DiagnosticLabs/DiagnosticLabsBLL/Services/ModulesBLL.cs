using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class ModulesBLL
    {
        private const string _logFileName = "ModulesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public ModulesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public Module GetModule(long id)
        {
            try
            {
                return _dbContext.Modules.Find(id);
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<Module> GetModules()
        {
            try
            {
                return _dbContext.Modules.Where(m => m.IsActive).OrderBy(m => m.SortOrder).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }
    }
}
