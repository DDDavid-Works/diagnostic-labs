using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class MultiLineEntriesBLL
    {
        private const string _logFileName = "MultiLineEntriesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public MultiLineEntriesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public List<MultiLineEntry> GetMultiLineEntries(int? moduleId, string fieldName)
        {
            try
            {
                return _dbContext.MultiLineEntries.Where(s => s.ModuleId == moduleId && s.FieldName == fieldName && s.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SaveMultiLineEntry(MultiLineEntry MultiLineEntry, ref long id)
        {
            try
            {
                if (MultiLineEntry.Id == 0)
                {
                    MultiLineEntry.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    MultiLineEntry.CreatedDate = DateTime.Now;
                    MultiLineEntry.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    MultiLineEntry.UpdatedDate = DateTime.Now;
                    _dbContext.MultiLineEntries.Add(MultiLineEntry);
                }
                else
                {
                    MultiLineEntry.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    MultiLineEntry.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = MultiLineEntry.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveMultiLineEntryList(List<MultiLineEntry> MultiLineEntries)
        {
            try
            {
                int? moduleId = MultiLineEntries.Select(s => s.ModuleId).Distinct().First();
                string fieldName = MultiLineEntries.Select(s => s.FieldName).Distinct().First();
                
                moduleId = moduleId == 0 ? null : moduleId;
                List<MultiLineEntry> existingMultiLineEntries = GetMultiLineEntries(moduleId, fieldName);
                List<long> existingMultiLineEntryIds = existingMultiLineEntries.Select(s => s.Id).ToList();

                List<MultiLineEntry> MultiLineEntriesToRemove = existingMultiLineEntries.Where(s => !MultiLineEntries.Select(sl => sl.Id).Contains(s.Id) && s.Id != 0).ToList();

                foreach (MultiLineEntry MultiLineEntry in MultiLineEntriesToRemove)
                {
                    long MultiLineEntryId = 0;
                    MultiLineEntry.IsActive = false;
                    SaveMultiLineEntry(MultiLineEntry, ref MultiLineEntryId);
                }

                foreach (MultiLineEntry MultiLineEntry in MultiLineEntries)
                {
                    long MultiLineEntryId = 0;
                    SaveMultiLineEntry(MultiLineEntry, ref MultiLineEntryId);
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
