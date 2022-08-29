using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DiagnosticLabsBLL.Services
{
    public class SingleLineEntriesBLL
    {
        private const string LogFileName = "SingleLineEntriesBLL";

        CommonFunctions commonFunctions = new CommonFunctions();

        private static DatabaseContext dbContext;

        public SingleLineEntriesBLL()
        {
            dbContext = new DatabaseContext();
        }

        public List<SingleLineEntry> GetSingleLineEntries(int? moduleId, string fieldName)
        {
            try
            {
                return dbContext.SingleLineEntries.Where(s => s.ModuleId == moduleId && s.FieldName == fieldName && s.IsActive).ToList();
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return null;
            }
        }

        public bool SaveSingleLineEntry(SingleLineEntry singleLineEntry, ref long id)
        {
            try
            {
                if (singleLineEntry.Id == 0)
                {
                    singleLineEntry.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    singleLineEntry.CreatedDate = DateTime.Now;
                    singleLineEntry.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    singleLineEntry.UpdatedDate = DateTime.Now;
                    dbContext.SingleLineEntries.Add(singleLineEntry);
                }
                else
                {
                    singleLineEntry.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    singleLineEntry.UpdatedDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                id = singleLineEntry.Id;

                return true;
            }
            catch (Exception ex)
            {
                commonFunctions.LogException(LogFileName, ex);
                return false;
            }
        }

        public bool SaveSingleLineEntryList(List<SingleLineEntry> singleLineEntries)
        {
            try
            {
                int? moduleId = singleLineEntries.Select(s => s.ModuleId).Distinct().First();
                string fieldName = singleLineEntries.Select(s => s.FieldName).Distinct().First();
                
                moduleId = moduleId == 0 ? null : moduleId;
                List<SingleLineEntry> existingSingleLineEntries = GetSingleLineEntries(moduleId, fieldName);
                List<long> existingSingleLineEntryIds = existingSingleLineEntries.Select(s => s.Id).ToList();

                List<SingleLineEntry> singleLineEntriesToRemove = existingSingleLineEntries.Where(s => !singleLineEntries.Select(sl => sl.Id).Contains(s.Id) && s.Id != 0).ToList();

                foreach (SingleLineEntry singleLineEntry in singleLineEntriesToRemove)
                {
                    long singleLineEntryId = 0;
                    singleLineEntry.IsActive = false;
                    SaveSingleLineEntry(singleLineEntry, ref singleLineEntryId);
                }

                foreach (SingleLineEntry singleLineEntry in singleLineEntries)
                {
                    long singleLineEntryId = 0;
                    SaveSingleLineEntry(singleLineEntry, ref singleLineEntryId);
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
