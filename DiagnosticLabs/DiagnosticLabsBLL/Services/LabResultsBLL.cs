using DiagnosticLabsBLL.Constants;
using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class LabResultsBLL
    {
        private const string _logFileName = "PackagesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();
        DefaultValuesBLL _defaultValuesBLL = new DefaultValuesBLL(); 

        private static DatabaseContext _dbContext;

        public LabResultsBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public T NewRecord<T>(int moduleId)
        {
            if (typeof(T) == typeof(StoolFecalysis))
            {
                StoolFecalysis stoolFecalysis = new StoolFecalysis()
                {
                    Id = 0,
                    PatientId = 0,
                    PatientRegistrationId = 0,
                    PatientCode = string.Empty,
                    PatientName = string.Empty,
                    CompanyOrPhysician = string.Empty,
                    Age = string.Empty,
                    Sex = string.Empty,
                    DateRequested = DateTime.Now,
                    Photo = null,
                    Color = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.StoolFecalysisColor),
                    Consistency = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.StoolFecalysisConsistency),
                    Result = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, MultiLineEntries.StoolFecalysisResult),
                    Remarks = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, MultiLineEntries.StoolFecalysisRemarks),
                    MedicalTechnologist = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.MedicalTechnologist),
                    Pathologist = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.Pathologist),
                    IsActive = true
                };
                return (T)Convert.ChangeType(stoolFecalysis, typeof(T));
            }

            return (T)Convert.ChangeType(null, typeof(T));
        }

        public T Get<T>(long id)
        {
            try
            {
                if (typeof(T) == typeof(StoolFecalysis))
                    return (T)Convert.ChangeType(_dbContext.StoolFecalyses.Find(id), typeof(T));

                return (T)Convert.ChangeType(null, typeof(T));
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return (T)Convert.ChangeType(null, typeof(T));
            }
        }

        public T GetByPatientRegistrationId<T>(long patientRegistrationId)
        {
            try
            {
                if (typeof(T) == typeof(StoolFecalysis))
                    return (T)Convert.ChangeType(_dbContext.StoolFecalyses.Where(s => s.PatientRegistrationId == patientRegistrationId).FirstOrDefault(), typeof(T));

                return (T)Convert.ChangeType(null, typeof(T));
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return (T)Convert.ChangeType(null, typeof(T));
            }
        }


        public List<LabResult> GetLabResults(string service, string patientName, long? companyId, string companyName, DateTime? dateRequested)
        {
            try
            {
                return _dbContext.LabResults.Where(l => (l.Service == service) &&
                                                        (patientName != string.Empty ? l.PatientName.ToUpper().Contains(patientName.ToUpper()) : true) &&
                                                        (dateRequested != null ? ((DateTime)l.DateRequested).Date == ((DateTime)dateRequested).Date : true) &&
                                                        ((companyId != -1 && companyId != null ? l.CompanyId == companyId : true) ||
                                                         (string.IsNullOrEmpty(companyName) && companyName != "--ALL--" ? l.Company == companyName : true))).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public List<LabResult> GetLabResultsByPatientRegistrationId(long patientRegistrationId)
        {
            try
            {
                return _dbContext.LabResults.Where(l => l.PatientRegistrationId == patientRegistrationId && l.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool Save<T>(T record, ref long id)
        {
            try
            {
                Type type = typeof(T);

                if ((long)type.GetProperty("Id").GetValue(record, null) == 0)
                {
                    type.GetProperty("CreatedByUserId").SetValue(record, Globals.Globals.LOGGEDINUSERID);
                    type.GetProperty("CreatedDate").SetValue(record, DateTime.Now);
                    type.GetProperty("UpdatedByUserId").SetValue(record, Globals.Globals.LOGGEDINUSERID);
                    type.GetProperty("UpdatedDate").SetValue(record, DateTime.Now);
                }
                else
                {
                    type.GetProperty("UpdatedByUserId").SetValue(record, Globals.Globals.LOGGEDINUSERID);
                    type.GetProperty("UpdatedDate").SetValue(record, DateTime.Now);
                }

                if (typeof(T) == typeof(StoolFecalysis))
                {
                    StoolFecalysis stoolFecalysis = record as StoolFecalysis;
                    if (stoolFecalysis.Id == 0)
                        _dbContext.StoolFecalyses.Add(stoolFecalysis);
                    
                    _dbContext.SaveChanges();
                    id = stoolFecalysis.Id;
                }

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveLabResultWithPatientRegistrationAndPatient<T>(T record, PatientRegistration patientRegistration, Patient patient, ref long id)
        {
            try
            {
                Type type = typeof(T);

                if (typeof(T) == typeof(StoolFecalysis))
                {
                    long? stoolFecalysisPatientId = patient?.Id, 
                        stoolFecalysisPatientRegistrationId = patientRegistration?.Id;
                    string stoolFecalysisPatientCode = patient?.PatientCode,
                        stoolFecalysisPatientName = patient?.PatientName,
                        stoolFecalysisAge = patient?.Age,
                        stoolFecalysisSex = patient?.Gender;

                    type.GetProperty("PatientId").SetValue(record, stoolFecalysisPatientId);
                    type.GetProperty("PatientRegistrationId").SetValue(record, stoolFecalysisPatientRegistrationId);
                    type.GetProperty("PatientCode").SetValue(record, stoolFecalysisPatientCode);
                    type.GetProperty("PatientName").SetValue(record, stoolFecalysisPatientName);
                    type.GetProperty("Age").SetValue(record, stoolFecalysisAge);
                    type.GetProperty("Sex").SetValue(record, stoolFecalysisSex);

                    return Save<StoolFecalysis>(record as StoolFecalysis, ref id);
                }

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
