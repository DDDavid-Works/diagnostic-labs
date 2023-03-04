using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class DefaultValuesBLL
    {
        private const string _logFileName = "DefaultValuesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public DefaultValuesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public DefaultValue NewDefaultValue(int moduleId, string fieldName, string fieldValueTitle, string fieldValue)
        {
            return new DefaultValue()
            {
                ModuleId = moduleId,
                FieldName = fieldName,
                FieldValueTitle = fieldValueTitle,
                FieldValue = fieldValue,
                IsActive = true
            };
        }

        public DefaultValue GetDefaultValuesByModuleIdAndFieldName(int? moduleId, string fieldName)
        {
            try
            {
                return _dbContext.DefaultValues.Where(s => s.ModuleId == moduleId && s.FieldName == fieldName && s.IsActive).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public string GetDefaultValueFieldValue(int? moduleId, string fieldName)
        {
            try
            {
                DefaultValue defaultValue = this.GetDefaultValuesByModuleIdAndFieldName(moduleId, fieldName);

                if (defaultValue == null)
                    return string.Empty;
                else
                    return defaultValue.FieldValue.ToString();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return string.Empty;
            }
        }

        public bool SaveDefaultValue(DefaultValue newDefaultValue, DefaultValue oldDefaultValue, bool hasNoDefault, ref long id)
        {
            try
            {
                if (hasNoDefault)
                {
                    if (oldDefaultValue != null)
                    {
                        oldDefaultValue.IsActive = false;
                        oldDefaultValue.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                        newDefaultValue.UpdatedDate = DateTime.Now;
                        _dbContext.SaveChanges();

                        id = oldDefaultValue.Id;
                    }
                }
                else
                {
                    if (newDefaultValue.Id == 0)
                    {
                        newDefaultValue.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                        newDefaultValue.CreatedDate = DateTime.Now;
                        newDefaultValue.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                        newDefaultValue.UpdatedDate = DateTime.Now;
                        _dbContext.DefaultValues.Add(newDefaultValue);
                    }
                    else
                    {
                        newDefaultValue.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                        newDefaultValue.UpdatedDate = DateTime.Now;
                    }
                    _dbContext.SaveChanges();

                    id = newDefaultValue.Id;
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
