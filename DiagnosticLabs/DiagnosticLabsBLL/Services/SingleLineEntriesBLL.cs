using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class SingleLineEntriesBLL
    {
        private const string _logFileName = "SingleLineEntriesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();
        DefaultValuesBLL _defaultValuesBLL = new DefaultValuesBLL();

        private static DatabaseContext _dbContext;

        public SingleLineEntriesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public List<SingleLineEntry> GetSingleLineEntries(int? moduleId, string fieldName, int openModuleId = 0)
        {
            try
            {
                List<SingleLineEntry> singleLineEntries = _dbContext.SingleLineEntries.Where(s => s.ModuleId == moduleId && s.FieldName == fieldName && s.IsActive).ToList();

                if (openModuleId != 0)
                {
                    DefaultValue defaultValue = _defaultValuesBLL.GetDefaultValuesByModuleIdAndFieldName(openModuleId, fieldName);

                    if (defaultValue != null)
                    {
                        foreach (var singleLineEntry in singleLineEntries)
                            singleLineEntry.IsDefault = singleLineEntry.FieldValue == defaultValue.FieldValue;
                    }
                }

                return singleLineEntries;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
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
                    _dbContext.SingleLineEntries.Add(singleLineEntry);
                }
                else
                {
                    singleLineEntry.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    singleLineEntry.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = singleLineEntry.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
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
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveSingleLineEntryListAndDefault(List<SingleLineEntry> singleLineEntries, DefaultValue defaultValue, DefaultValue originalDefaultValue, bool hasNoDefault)
        {
            try
            {
                long defaultValueId = 0;
                if (_defaultValuesBLL.SaveDefaultValue(defaultValue, originalDefaultValue, hasNoDefault, ref defaultValueId))
                {
                    if (SaveSingleLineEntryList(singleLineEntries))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }
    }
}
