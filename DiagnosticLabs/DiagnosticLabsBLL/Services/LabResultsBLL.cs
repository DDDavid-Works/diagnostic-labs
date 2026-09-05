using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class LabResultsBLL
    {
        private const string _logFileName = "LabResultsBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();
        PatientsBLL _patientBLL = new PatientsBLL();

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
                    return (T)Convert.ChangeType(NewStoolFecalysis(defaultsJson, isForSetDefaults), typeof(T));
                }
                else if (typeof(T) == typeof(Urinalysis))
                {
                    return (T)Convert.ChangeType(NewUrinalysis(defaultsJson, isForSetDefaults), typeof(T));
                }
                else if (typeof(T) == typeof(APE))
                {
                    return (T)Convert.ChangeType(NewAPE(defaultsJson, isForSetDefaults), typeof(T));
                }
                else if (typeof(T) == typeof(MER))
                {
                    return (T)Convert.ChangeType(NewMER(defaultsJson, isForSetDefaults), typeof(T));
                }
                else if (typeof(T) == typeof(ClinicalChemistry))
                {
                    return (T)Convert.ChangeType(NewClinicalChemistry(defaultsJson, isForSetDefaults), typeof(T));
                }

                return (T)Convert.ChangeType(null, typeof(T));

            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return (T)Convert.ChangeType(null, typeof(T));
            }
        }

        #region Labs
        public StoolFecalysis NewStoolFecalysis(string defaultsJson, bool isForSetDefaults)
        {
            if (string.IsNullOrEmpty(defaultsJson))
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
                    Color = string.Empty,
                    Consistency = string.Empty,
                    Result = string.Empty,
                    Remarks = string.Empty,
                    MedicalTechnologist = string.Empty,
                    Pathologist = string.Empty,
                    IsActive = true
                };
                return stoolFecalysis;
            }
            else
            {
                StoolFecalysis stoolFecalysis = Newtonsoft.Json.JsonConvert.DeserializeObject<StoolFecalysis>(defaultsJson);

                if (isForSetDefaults)
                {
                    stoolFecalysis.PatientId = 0;
                    stoolFecalysis.PatientRegistrationId = 0;
                    stoolFecalysis.PatientCode = string.Empty;
                    stoolFecalysis.PatientName = string.Empty;
                    stoolFecalysis.CompanyOrPhysician = string.Empty;
                    stoolFecalysis.Age = string.Empty;
                    stoolFecalysis.Sex = string.Empty;
                    stoolFecalysis.Photo = null;
                }
                stoolFecalysis.DateRequested = isForSetDefaults ? null : DateTime.Now;
                return stoolFecalysis;
            }
        }

        public Urinalysis NewUrinalysis(string defaultsJson, bool isForSetDefaults)
        {
            if (string.IsNullOrEmpty(defaultsJson))
            {
                Urinalysis urinalysis = new Urinalysis()
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
                    Color = string.Empty,
                    Appearance = string.Empty,
                    Reaction = string.Empty,
                    SPGravity = string.Empty,
                    Albumin = string.Empty,
                    Sugar = string.Empty,
                    PusCells = string.Empty,
                    RedCells = string.Empty,
                    MucusThreads = string.Empty,
                    EpithelialCells = string.Empty,
                    AmorphousUratesPO4 = string.Empty,
                    Bacteria = string.Empty,
                    Casts = string.Empty,
                    Crystals = string.Empty,
                    Others = string.Empty,
                    Remarks = string.Empty,
                    MedicalTechnologist = string.Empty,
                    Pathologist = string.Empty,
                    IsActive = true
                };
                return urinalysis;
            }
            else
            {
                Urinalysis urinalysis = Newtonsoft.Json.JsonConvert.DeserializeObject<Urinalysis>(defaultsJson);

                if (isForSetDefaults)
                {
                    urinalysis.PatientId = 0;
                    urinalysis.PatientRegistrationId = 0;
                    urinalysis.PatientCode = string.Empty;
                    urinalysis.PatientName = string.Empty;
                    urinalysis.CompanyOrPhysician = string.Empty;
                    urinalysis.Age = string.Empty;
                    urinalysis.Sex = string.Empty;
                    urinalysis.Photo = null;
                }
                urinalysis.DateRequested = isForSetDefaults ? null : DateTime.Now;
                return urinalysis;
            }
        }

        public APE NewAPE(string defaultsJson, bool isForSetDefaults)
        {
            if (string.IsNullOrEmpty(defaultsJson))
            {
                APE ape = new APE()
                {
                    Id = 0,
                    PatientId = 0,
                    PatientRegistrationId = 0,
                    DateInputted = isForSetDefaults ? null : DateTime.Now,
                    PatientName = string.Empty,
                    CompanyName = string.Empty,
                    DepartmentOrAgency = string.Empty,
                    Age = string.Empty,
                    BirthDate = null,
                    Gender = string.Empty,
                    CivilStatus = string.Empty,
                    ContactNo = string.Empty,
                    ENT = string.Empty,
                    Gastroenterology = string.Empty,
                    Respiratory = string.Empty,
                    IntegumentarySkin = string.Empty,
                    Cardiology = string.Empty,
                    Psychology = string.Empty,
                    Endocrinology = string.Empty,
                    OBGyneUrology = string.Empty,
                    Muscoloskeletal = string.Empty,
                    InfectiousCommunicable = string.Empty,
                    Neurological = string.Empty,
                    Surgical = string.Empty,
                    OthersPast = string.Empty,
                    Medications = string.Empty,
                    ReviewOfSystems = string.Empty,
                    Allergies = string.Empty,
                    IsSmoking = false,
                    SmokingSinceWhen = string.Empty,
                    NumberOfSticksPerDay = null,
                    DrinkingSinceWhen = string.Empty,
                    NumberOfBottles = null,
                    DrinkingFrequency = string.Empty,
                    LMP = string.Empty,
                    LMPType = string.Empty,
                    BP1st = string.Empty,
                    BP2nd = string.Empty,
                    CardiacRate1st = string.Empty,
                    CardiacRate2nd = string.Empty,
                    Height = string.Empty,
                    Weight = string.Empty,
                    BMICategory = string.Empty,
                    VARightEyeWGlasses = string.Empty,
                    VARightEyeWOGlasses = string.Empty,
                    VALeftEyeWGlasses = string.Empty,
                    VALeftEyeWOGlasses = string.Empty,
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
                    Findings = string.Empty,
                    VitalSignsBy = string.Empty,
                    HeightWeightBy = string.Empty,
                    IsActive = true
                };
                return ape;
            }
            else
            {
                APE ape = Newtonsoft.Json.JsonConvert.DeserializeObject<APE>(defaultsJson);

                if (isForSetDefaults)
                {
                    ape.PatientId = 0;
                    ape.PatientRegistrationId = 0;
                    ape.PatientName = string.Empty;
                    ape.CompanyName = string.Empty;
                    ape.DepartmentOrAgency = string.Empty;
                    ape.Age = string.Empty;
                    ape.BirthDate = null;
                    ape.Gender = string.Empty;
                    ape.CivilStatus = string.Empty;
                    ape.ContactNo = string.Empty;
                }
                ape.DateInputted = isForSetDefaults ? null : DateTime.Now;
                return ape;
            }
        }

        public MER NewMER(string defaultsJson, bool isForSetDefaults)
        {
            if (string.IsNullOrEmpty(defaultsJson))
            {
                MER mer = new MER()
                {
                    Id = 0,
                    PatientId = 0,
                    PatientRegistrationId = 0,
                    DateInputted = isForSetDefaults ? null : DateTime.Now,
                    PatientName = string.Empty,
                    ContactNo = string.Empty,
                    Age = string.Empty,
                    Gender = string.Empty,
                    CivilStatus = string.Empty,
                    CompanyName = string.Empty,
                    ChestXray = string.Empty,
                    ChestXrayRemarks = string.Empty,
                    CBC = string.Empty,
                    CBCRemarks = string.Empty,
                    Urinalysis = string.Empty,
                    UrinalysisRemarks = string.Empty,
                    Fecalysis = string.Empty,
                    FecalysisRemarks = string.Empty,
                    HBsAg = string.Empty,
                    HBsAgRemarks = string.Empty,
                    DrugTest2Panel = string.Empty,
                    DrugTest2PanelRemarks = string.Empty,
                    DrugTest4Panel = string.Empty,
                    DrugTest4PanelRemarks = string.Empty,
                    Classification = string.Empty,
                    MedicalSurgicalHistory = string.Empty,
                    Assessment = string.Empty,
                    Remarks = string.Empty,
                    AssessmentDoneBy = string.Empty,
                    PhysicianName = string.Empty,
                    PhysicianLicense = string.Empty,
                    IsActive = true
                };
                return mer;
            }
            else
            {
                MER mer = Newtonsoft.Json.JsonConvert.DeserializeObject<MER>(defaultsJson);

                if (isForSetDefaults)
                {
                    mer.PatientId = 0;
                    mer.PatientRegistrationId = 0;
                    mer.PatientName = string.Empty;
                    mer.ContactNo = string.Empty;
                    mer.Age = string.Empty;
                    mer.Gender = string.Empty;
                    mer.CivilStatus = string.Empty;
                    mer.CompanyName = string.Empty;
                }
                mer.DateInputted = isForSetDefaults ? null : DateTime.Now;
                return mer;
            }
        }

        public ClinicalChemistry NewClinicalChemistry(string defaultsJson, bool isForSetDefaults)
        {
            if (string.IsNullOrEmpty(defaultsJson))
            {
                ClinicalChemistry clinicalChemistry = new ClinicalChemistry()
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
                    FBSNValue = string.Empty,
                    FBSResult = string.Empty,
                    TotalCholesterolNValue = string.Empty,
                    TotalCholesterolResult = string.Empty,
                    TriglyceridesNValue = string.Empty,
                    TriglyceridesResult = string.Empty,
                    HDLNValue = string.Empty,
                    HDLResult = string.Empty,
                    BUNNValue = string.Empty,
                    BUNResult = string.Empty,
                    CreatinineNValue = string.Empty,
                    CreatinineResult = string.Empty,
                    BloodUricAcidNValue = string.Empty,
                    BloodUricAcidResult = string.Empty,
                    LDLNValue = string.Empty,
                    LDLResult = string.Empty,
                    SGPTNValue = string.Empty,
                    SGPTResult = string.Empty,
                    MedicalTechnologist = string.Empty,
                    Pathologist = string.Empty,
                    IsActive = true
                };

                return clinicalChemistry;
            }
            else
            {
                ClinicalChemistry clinicalChemistry = Newtonsoft.Json.JsonConvert.DeserializeObject<ClinicalChemistry>(defaultsJson);

                if (isForSetDefaults)
                {
                    clinicalChemistry.PatientId = 0;
                    clinicalChemistry.PatientRegistrationId = 0;
                    clinicalChemistry.PatientCode = string.Empty;
                    clinicalChemistry.PatientName = string.Empty;
                    clinicalChemistry.CompanyOrPhysician = string.Empty;
                    clinicalChemistry.Age = string.Empty;
                    clinicalChemistry.Sex = string.Empty;
                }
                clinicalChemistry.DateRequested = isForSetDefaults ? null : DateTime.Now;
                return clinicalChemistry;
            }
        }
        #endregion

        public T Get<T>(long id)
        {
            try
            {
                if (typeof(T) == typeof(StoolFecalysis))
                    return (T)Convert.ChangeType(_dbContext.StoolFecalyses.AsNoTracking().FirstOrDefault(r => r.Id == id), typeof(T));
                else if (typeof(T) == typeof(Urinalysis))
                    return (T)Convert.ChangeType(_dbContext.Urinalyses.AsNoTracking().FirstOrDefault(r => r.Id == id), typeof(T));
                else if (typeof(T) == typeof(APE))
                    return (T)Convert.ChangeType(_dbContext.APEs.AsNoTracking().FirstOrDefault(r => r.Id == id), typeof(T));
                else if (typeof(T) == typeof(MER))
                    return (T)Convert.ChangeType(_dbContext.MERs.AsNoTracking().FirstOrDefault(r => r.Id == id), typeof(T));
                else if (typeof(T) == typeof(ClinicalChemistry))
                    return (T)Convert.ChangeType(_dbContext.ClinicalChemistries.AsNoTracking().FirstOrDefault(r => r.Id == id), typeof(T));

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
                else if (typeof(T) == typeof(Urinalysis))
                    return (T)Convert.ChangeType(_dbContext.Urinalyses.Where(s => s.PatientRegistrationId == patientRegistrationId).FirstOrDefault(), typeof(T));
                else if (typeof(T) == typeof(APE))
                    return (T)Convert.ChangeType(_dbContext.APEs.Where(a => a.PatientRegistrationId == patientRegistrationId).FirstOrDefault(), typeof(T));
                else if (typeof(T) == typeof(MER))
                    return (T)Convert.ChangeType(_dbContext.MERs.Where(a => a.PatientRegistrationId == patientRegistrationId).FirstOrDefault(), typeof(T));
                else if (typeof(T) == typeof(ClinicalChemistry))
                    return (T)Convert.ChangeType(_dbContext.ClinicalChemistries.Where(a => a.PatientRegistrationId == patientRegistrationId).FirstOrDefault(), typeof(T));

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
                    else
                        _dbContext.StoolFecalyses.Update(stoolFecalysis);
                }
                else if (typeof(T) == typeof(Urinalysis))
                {
                    Urinalysis urinalysis = record as Urinalysis;
                    if (urinalysis.Id == 0)
                        _dbContext.Urinalyses.Add(urinalysis);
                    else
                        _dbContext.Urinalyses.Update(urinalysis);
                }
                else if (typeof(T) == typeof(APE))
                {
                    APE ape = record as APE;
                    if (ape.Id == 0)
                        _dbContext.APEs.Add(ape);
                    else
                        _dbContext.APEs.Update(ape);
                }
                else if (typeof(T) == typeof(MER))
                {
                    MER mer = record as MER;
                    if (mer.Id == 0)
                        _dbContext.MERs.Add(mer);
                    else
                        _dbContext.MERs.Update(mer);
                }
                else if (typeof(T) == typeof(ClinicalChemistry))
                {
                    ClinicalChemistry cc = record as ClinicalChemistry;
                    if (cc.Id == 0)
                        _dbContext.ClinicalChemistries.Add(cc);
                    else
                        _dbContext.ClinicalChemistries.Update(cc);
                }

                _dbContext.SaveChanges();

                id = (long)type.GetProperty("Id").GetValue(record, null);

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SaveLabResult<T>(T record, PatientRegistration patientRegistration, Patient patient, ref long id)
        {
            try
            {
                long patiendId = 0;
                _patientBLL.SavePatient(patient, ref patiendId);

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
                else if (typeof(T) == typeof(Urinalysis))
                {
                    long? urinalysisPatientId = patient?.Id,
                        urinalysisPatientRegistrationId = patientRegistration?.Id;
                    string urinalysisPatientCode = patient?.PatientCode,
                        urinalysisPatientName = patient?.PatientName,
                        urinalysisAge = patient?.Age,
                        urinalysisSex = patient?.Gender,
                        urinalysisCompanyOrPhysician = patient?.CompanyName;

                    type.GetProperty("PatientId").SetValue(record, urinalysisPatientId);
                    type.GetProperty("PatientRegistrationId").SetValue(record, urinalysisPatientRegistrationId);
                    type.GetProperty("PatientCode").SetValue(record, urinalysisPatientCode);
                    type.GetProperty("PatientName").SetValue(record, urinalysisPatientName);
                    type.GetProperty("Age").SetValue(record, urinalysisAge);
                    type.GetProperty("Sex").SetValue(record, urinalysisSex);
                    type.GetProperty("CompanyOrPhysician").SetValue(record, urinalysisCompanyOrPhysician);

                    return Save<Urinalysis>(record as Urinalysis, ref id);
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
                else if (typeof(T) == typeof(MER))
                {
                    long? merPatientId = patient?.Id,
                        merPatientRegistrationId = patientRegistration?.Id;
                    string merPatientName = patient?.PatientName,
                        merContactNo = patient?.ContactNumbers,
                        merAge = patient?.Age,
                        merGender = patient?.Gender,
                        merCivilStatus = patient?.CivilStatus,
                        merCompanyName = patient?.CompanyName;

                    type.GetProperty("PatientId").SetValue(record, merPatientId);
                    type.GetProperty("PatientRegistrationId").SetValue(record, merPatientRegistrationId);
                    type.GetProperty("PatientName").SetValue(record, merPatientName);
                    type.GetProperty("ContactNo").SetValue(record, merContactNo);
                    type.GetProperty("Age").SetValue(record, merAge);
                    type.GetProperty("Gender").SetValue(record, merGender);
                    type.GetProperty("CivilStatus").SetValue(record, merCivilStatus);
                    type.GetProperty("CompanyName").SetValue(record, merCompanyName);

                    return Save<MER>(record as MER, ref id);
                }
                else if (typeof(T) == typeof(ClinicalChemistry))
                {
                    long? clinicalChemistryPatientId = patient?.Id,
                        clinicalChemistryPatientRegistrationId = patientRegistration?.Id;
                    string clinicalChemistryPatientCode = patient?.PatientCode,
                        clinicalChemistryPatientName = patient?.PatientName,
                        clinicalChemistryCompanyOrPhysician = patient?.CompanyName,
                        clinicalChemistryAge = patient?.Age,
                        clinicalChemistrySex = patient?.Gender;

                    type.GetProperty("PatientId").SetValue(record, clinicalChemistryPatientId);
                    type.GetProperty("PatientRegistrationId").SetValue(record, clinicalChemistryPatientRegistrationId);
                    type.GetProperty("PatientCode").SetValue(record, clinicalChemistryPatientCode);
                    type.GetProperty("PatientName").SetValue(record, clinicalChemistryPatientName);
                    type.GetProperty("CompanyOrPhysician").SetValue(record, clinicalChemistryCompanyOrPhysician);
                    type.GetProperty("Age").SetValue(record, clinicalChemistryAge);
                    type.GetProperty("Sex").SetValue(record, clinicalChemistrySex);

                    return Save<ClinicalChemistry>(record as ClinicalChemistry, ref id);
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
