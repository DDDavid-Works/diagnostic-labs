using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class LabResultsDefaultsBLL
    {
        private const string _logFileName = "LabResultsDefaultsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public LabResultsDefaultsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public LabResultsDefault NewLabResultsDefault()
        {
            return new LabResultsDefault()
            {
                Id = 0,
                ModuleId = 0,
                Defaults = string.Empty,
                IsActive = true,
            };
        }

        public LabResultsDefault GetLabResultsDefault(long id)
        {
            try
            {
                LabResultsDefault labResultsDefault = _dbContext.LabResultsDefaults.Find(id);

                return labResultsDefault;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public LabResultsDefault GetLabResultsDefaultByModuleId(long moduleId)
        {
            try
            {
                return _dbContext.LabResultsDefaults.Where(d => d.IsActive && d.ModuleId == moduleId).OrderBy(d => d.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<LabResultsDefault> GetAllLabResultsDefaults()
        {
            try
            {
                return _dbContext.LabResultsDefaults.Where(p => p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SaveLabResultsDefault(LabResultsDefault labResultsDefault, ref long id)
        {
            try
            {
                if (labResultsDefault.Id == 0)
                {
                    labResultsDefault.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    labResultsDefault.CreatedDate = DateTime.Now;
                    labResultsDefault.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    labResultsDefault.UpdatedDate = DateTime.Now;
                    _dbContext.LabResultsDefaults.Add(labResultsDefault);
                }
                else
                {
                    labResultsDefault.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    labResultsDefault.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = labResultsDefault.Id;

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
