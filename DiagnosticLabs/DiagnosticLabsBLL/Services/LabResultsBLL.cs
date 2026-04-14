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

        public T NewRecord<T>(int moduleId, string defaultsJson = null, bool isForSetDefaults = false)
        {
            try
            {
                if (typeof(T) == typeof(StoolFecalysis))
                {
                    if (defaultsJson == null)
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
                            DateRequested = isForSetDefaults ? null : DateTime.Now,
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
                    else
                    {
                        StoolFecalysis stoolFecalysis = Newtonsoft.Json.JsonConvert.DeserializeObject<StoolFecalysis>(defaultsJson);
                        return (T)Convert.ChangeType(stoolFecalysis, typeof(T));
                    }
                }
                else if (typeof(T) == typeof(APE))
                {
                    APE ape = new APE()
                    {
                        Id = 0,
                        PatientId = 0,
                        PatientRegistrationId = 0,
                        DateInputted = null,
                        PatientName = string.Empty,
                        CompanyName = string.Empty,
                        DepartmentOrAgency = string.Empty,
                        Age = string.Empty,
                        BirthDate = null,
                        Gender = string.Empty,
                        CivilStatus = string.Empty,
                        ContactNo = string.Empty,
                        ENT = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEENT),
                        Gastroenterology = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEGastroenterology),
                        Respiratory = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APERespiratory),
                        IntegumentarySkin = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEIntegumentarySkin),
                        Cardiology = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APECardiology),
                        Psychology = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEPsychology),
                        Endocrinology = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEEndocrinology),
                        OBGyneUrology = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEOBGyneUrology),
                        Muscoloskeletal = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEMuscoloskeletal),
                        InfectiousCommunicable = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEInfectiousCommunicable),
                        Neurological = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APENeurological),
                        Surgical = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APESurgical),
                        OthersPast = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, MultiLineEntries.APEOthersPast),
                        Medications = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, MultiLineEntries.APEMedications),
                        ReviewOfSystems = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, MultiLineEntries.APEReviewOfSystems),
                        Allergies = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, MultiLineEntries.APEAllergies),
                        IsSmoking = false,
                        SmokingSinceWhen = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APESmokingSinceWhen),
                        NumberOfSticksPerDay = null,
                        DrinkingSinceWhen = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEDrinkingSinceWhen),
                        NumberOfBottles = null,
                        DrinkingFrequency = string.Empty,
                        LMP = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APELMP),
                        LMPType = string.Empty,
                        BP1st = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEBP1st),
                        BP2nd = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEBP2nd),
                        CardiacRate1st = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APECardiacRate1st),
                        CardiacRate2nd = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APECardiacRate2nd),
                        Height = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEHeight),
                        Weight = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEWeight),
                        BMICategory = string.Empty,
                        VARightEyeWGlasses = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEVARightEyeWGlasses),
                        VARightEyeWOGlasses = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEVARightEyeWOGlasses),
                        VALeftEyeWGlasses = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEVALeftEyeWGlasses),
                        VALeftEyeWOGlasses = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APEVALeftEyeWOGlasses),
                        VisualAcuity = string.Empty,
                        Skin = string.Empty,
                        HeadScalp = string.Empty,
                        Eyes = string.Empty,
                        Ears = string.Empty,
                        Nose = string.Empty,
                        TeethTonsilsThroatPharynx = string.Empty,
                        NeckLymphNodesThyroid = string.Empty,
                        ThoraxBreast = string.Empty,
                        HeartLungs = string.Empty,
                        AbdomenLiverSpleen = string.Empty,
                        InguinalAreaGenitalsAnus = string.Empty,
                        ExtremetiesSpine = string.Empty,
                        Tattoo = string.Empty,
                        MassCyst = string.Empty,
                        OthersPE = string.Empty,
                        Findings = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, MultiLineEntries.APEFindings),
                        VitalSignsBy = string.Empty,
                        HeightWeightBy = string.Empty,
                        IsActive = true,
                        APENumberOfSticksPerDay = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APENumberOfSticksPerDay),
                        APENumberOfBottles = _defaultValuesBLL.GetDefaultValueFieldValue(moduleId, SingleLineEntries.APENumberOfBottles)
                    };
                    return (T)Convert.ChangeType(ape, typeof(T));
                }

                return (T)Convert.ChangeType(null, typeof(T));

            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return (T)Convert.ChangeType(null, typeof(T));
            }
        }

        public T Get<T>(long id)
        {
            try
            {
                if (typeof(T) == typeof(StoolFecalysis))
                    return (T)Convert.ChangeType(_dbContext.StoolFecalyses.Find(id), typeof(T));
                else if (typeof(T) == typeof(APE))
                    return (T)Convert.ChangeType(_dbContext.APEs.Find(id), typeof(T));

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
                else if (typeof(T) == typeof(APE))
                    return (T)Convert.ChangeType(_dbContext.APEs.Where(a => a.PatientRegistrationId == patientRegistrationId).FirstOrDefault(), typeof(T));

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
                    
                    _dbContext.SaveChangesAsync();
                    id = stoolFecalysis.Id;
                }
                else if (typeof(T) == typeof(APE))
                {
                    APE ape = record as APE;
                    if (ape.Id == 0)
                        _dbContext.APEs.Add(ape);

                    _dbContext.SaveChangesAsync();
                    id = ape.Id;
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
                        stoolFecalysisSex = patient?.Gender,
                        stoolFecalysisCompanyOrPhysician = patient?.CompanyName;

                    type.GetProperty("PatientId").SetValue(record, stoolFecalysisPatientId);
                    type.GetProperty("PatientRegistrationId").SetValue(record, stoolFecalysisPatientRegistrationId);
                    type.GetProperty("PatientCode").SetValue(record, stoolFecalysisPatientCode);
                    type.GetProperty("PatientName").SetValue(record, stoolFecalysisPatientName);
                    type.GetProperty("Age").SetValue(record, stoolFecalysisAge);
                    type.GetProperty("Sex").SetValue(record, stoolFecalysisSex);
                    type.GetProperty("CompanyOrPhysician").SetValue(record, stoolFecalysisCompanyOrPhysician);

                    return Save<StoolFecalysis>(record as StoolFecalysis, ref id);
                }
                else if (typeof(T) == typeof(APE))
                {
                    long? apePatientId = patient?.Id,
                        apePatientRegistrationId = patientRegistration?.Id;
                    string apePatientName = patient?.PatientName,
                        apeAge = patient?.Age,
                        apeGender = patient?.Gender,
                        apeCivilStatus = patient?.CivilStatus,
                        apeCompanyName = patient?.CompanyName;
                    DateTime? apeBirthDate = patient?.DateOfBirth;

                    type.GetProperty("PatientId").SetValue(record, apePatientId);
                    type.GetProperty("PatientRegistrationId").SetValue(record, apePatientRegistrationId);
                    type.GetProperty("PatientName").SetValue(record, apePatientName);
                    type.GetProperty("Age").SetValue(record, apeAge);
                    type.GetProperty("BirthDate").SetValue(record, apeBirthDate);
                    type.GetProperty("Gender").SetValue(record, apeGender);
                    type.GetProperty("CivilStatus").SetValue(record, apeCivilStatus);
                    type.GetProperty("CompanyName").SetValue(record, apeCompanyName);

                    return Save<APE>(record as APE, ref id);
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
