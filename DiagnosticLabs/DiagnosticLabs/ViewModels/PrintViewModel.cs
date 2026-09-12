using CrystalDecisions.CrystalReports.Engine;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabs.Models.Reports;
using System;
using System.Linq;

namespace DiagnosticLabs.ViewModels
{
    public class PrintViewModel : BaseViewModel
    {
        private const string _entityName = "Print";

        CompanySetupBLL _companySetupBLL = new CompanySetupBLL();
        LabResultsBLL _labResults = new LabResultsBLL();

        #region Public Properties
        public ReportDocument ReportDocument { get; set; } = new ReportDocument();
        #endregion

        public PrintViewModel(string module, long recordId)
        {
            var record = (object)null;
            string appPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName).Replace("\\", "/");

            CompanySetup companySetup = _companySetupBLL.GetLatestCompanySetup();

            switch (module)
            {
                case Modules.CompanySetup:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/CompanySetup.rpt");
                    record = this.ReportObject<CompanySetup>(companySetup, companySetup);
                    break;
                case Modules.StoolFecalysis:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/StoolFecalysisReport.rpt");

                    StoolFecalysis stoolFecalysis = _labResults.Get<StoolFecalysis>(recordId);
                    record = this.ReportObject<StoolFecalysis>(stoolFecalysis, companySetup);
                    break;
                case Modules.Urinalysis:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/UrinalysisReport.rpt");

                    Urinalysis urinalysis = _labResults.Get<Urinalysis>(recordId);
                    record = this.ReportObject<Urinalysis>(urinalysis, companySetup);
                    break;
                case Modules.AnnualPhysicalExam:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/APEReport.rpt");

                    APE ape = _labResults.Get<APE>(recordId);
                    record = this.ReportObject<APE>(ape, companySetup);
                    break;
                case Modules.MedicalExamination:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/MedicalExaminationReport.rpt");

                    MER mer = _labResults.Get<MER>(recordId);
                    record = this.ReportObject<MER>(mer, companySetup);
                    break;
                case Modules.ClinicalChemistry:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/ClinicalChemistryReport.rpt");

                    ClinicalChemistry clinicalChemistry = _labResults.Get<ClinicalChemistry>(recordId);
                    record = this.ReportObject<ClinicalChemistry>(clinicalChemistry, companySetup);
                    break;
                case Modules.Hematology:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/HematologyReport.rpt");

                    Hematology hematology = _labResults.Get<Hematology>(recordId);
                    record = this.ReportObject<Hematology>(hematology, companySetup);
                    break;
                case Modules.ClinicalChemistry1:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/ClinicalChemistry1Report.rpt");

                    ClinicalChemistry1 clinicalChemistry1 = _labResults.Get<ClinicalChemistry1>(recordId);
                    record = this.ReportObject<ClinicalChemistry1>(clinicalChemistry1, companySetup);
                    break;
                case Modules.ClinicalChemistry2:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/ClinicalChemistry2Report.rpt");

                    ClinicalChemistry2 clinicalChemistry2 = _labResults.Get<ClinicalChemistry2>(recordId);
                    record = this.ReportObject<ClinicalChemistry2>(clinicalChemistry2, companySetup);
                    break;
                case Modules.Immunology:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/ImmunologyReport.rpt");

                    Immunology immunology = _labResults.Get<Immunology>(recordId);
                    record = this.ReportObject<Immunology>(immunology, companySetup);
                    break;
                case Modules.PregnancyTest:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/PregnancyTestReport.rpt");

                    PregnancyTest pregnancyTest = _labResults.Get<PregnancyTest>(recordId);
                    record = this.ReportObject<PregnancyTest>(pregnancyTest, companySetup);
                    break;
                case Modules.Serology:
                    this.ReportDocument.Load(appPath + "/Reports/LabResults/SerologyReport.rpt");

                    Serology serology = _labResults.Get<Serology>(recordId);
                    record = this.ReportObject<Serology>(serology, companySetup);
                    break;
                default:
                    break;
            }

            if (record != null)
            {
                this.ReportDocument.SetDataSource((new[] { record }).ToList());

                switch (module)
                {
                    case Modules.StoolFecalysis:
                    case Modules.Urinalysis:
                    case Modules.AnnualPhysicalExam:
                    case Modules.MedicalExamination:
                    case Modules.ClinicalChemistry:
                    case Modules.Hematology:
                    case Modules.ClinicalChemistry1:
                    case Modules.ClinicalChemistry2:
                    case Modules.Immunology:
                    case Modules.PregnancyTest:
                    case Modules.Serology:
                        this.ReportDocument.SetParameterValue("CompanyName", companySetup.CompanyName);
                        this.ReportDocument.SetParameterValue("SubCompanyName", companySetup.SubCompanyName);
                        this.ReportDocument.SetParameterValue("CompanyAddress", companySetup.Address);
                        this.ReportDocument.SetParameterValue("CompanyContactNumbers", companySetup.ContactNumbers);
                        this.ReportDocument.SetParameterValue("CompanyEmail", companySetup.Email);
                        break;
                    default:
                        break;
                }
            }
        }

        #region Private Methods
        private object ReportObject<T>(T record, CompanySetup companySetup)
        {
            object reportObject = null;

            Type type = typeof(T);

            if (typeof(T) == typeof(CompanySetup))
            {
                reportObject = new
                {
                    CompanyName = (string)type.GetProperty("CompanyName").GetValue(record),
                    Logo = (byte[])type.GetProperty("Logo").GetValue(record),
                    LogoImage = (byte[])type.GetProperty("LogoImage").GetValue(record),
                };
            }
            else if (typeof(T) == typeof(StoolFecalysis))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<StoolFecalysis, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Color = (string)type.GetProperty("Color").GetValue(record),
                    Consistency = (string)type.GetProperty("Consistency").GetValue(record),
                    Result = (string)type.GetProperty("Result").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(Urinalysis))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<Urinalysis, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Color = (string)type.GetProperty("Color").GetValue(record),
                    Appearance = (string)type.GetProperty("Appearance").GetValue(record),
                    Reaction = (string)type.GetProperty("Reaction").GetValue(record),
                    SPGravity = (string)type.GetProperty("SPGravity").GetValue(record),
                    Albumin = (string)type.GetProperty("Albumin").GetValue(record),
                    Sugar = (string)type.GetProperty("Sugar").GetValue(record),
                    PusCells = (string)type.GetProperty("PusCells").GetValue(record),
                    RedCells = (string)type.GetProperty("RedCells").GetValue(record),
                    MucusThreads = (string)type.GetProperty("MucusThreads").GetValue(record),
                    EpithelialCells = (string)type.GetProperty("EpithelialCells").GetValue(record),
                    AmorphousUratesPO4 = (string)type.GetProperty("AmorphousUratesPO4").GetValue(record),
                    Bacteria = (string)type.GetProperty("Bacteria").GetValue(record),
                    Casts = (string)type.GetProperty("Casts").GetValue(record),
                    Crystals = (string)type.GetProperty("Crystals").GetValue(record),
                    Others = (string)type.GetProperty("Others").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(APE))
            {
                reportObject = new
                {
                    DateInputted = GetDataFromRecord<APE, string>(record, "DateInputted", "MM/dd/yyyy"),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyName = (string)type.GetProperty("CompanyName").GetValue(record),
                    DepartmentOrAgency = (string)type.GetProperty("DepartmentOrAgency").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    BirthDate = GetDataFromRecord<APE, string>(record, "BirthDate", "MM/dd/yyyy"),
                    Gender = (string)type.GetProperty("Gender").GetValue(record),
                    CivilStatus = (string)type.GetProperty("CivilStatus").GetValue(record),
                    ContactNo = (string)type.GetProperty("ContactNo").GetValue(record),
                    ENT = (string)type.GetProperty("ENT").GetValue(record),
                    Gastroenterology = (string)type.GetProperty("Gastroenterology").GetValue(record),
                    Respiratory = (string)type.GetProperty("Respiratory").GetValue(record),
                    IntegumentarySkin = (string)type.GetProperty("IntegumentarySkin").GetValue(record),
                    Cardiology = (string)type.GetProperty("Cardiology").GetValue(record),
                    Psychology = (string)type.GetProperty("Psychology").GetValue(record),
                    Endocrinology = (string)type.GetProperty("Endocrinology").GetValue(record),
                    OBGyneUrology = (string)type.GetProperty("OBGyneUrology").GetValue(record),
                    Muscoloskeletal = (string)type.GetProperty("Muscoloskeletal").GetValue(record),
                    InfectiousCommunicable = (string)type.GetProperty("InfectiousCommunicable").GetValue(record),
                    Neurological = (string)type.GetProperty("Neurological").GetValue(record),
                    Surgical = (string)type.GetProperty("Surgical").GetValue(record),
                    OthersPast = (string)type.GetProperty("OthersPast").GetValue(record),
                    Medications = (string)type.GetProperty("Medications").GetValue(record),
                    ReviewOfSystems = (string)type.GetProperty("ReviewOfSystems").GetValue(record),
                    Allergies = (string)type.GetProperty("Allergies").GetValue(record),
                    IsSmoking = (bool)type.GetProperty("IsSmoking").GetValue(record),
                    SmokingSinceWhen = (string)type.GetProperty("SmokingSinceWhen").GetValue(record),
                    NumberOfSticksPerDay = ((int?)type.GetProperty("NumberOfSticksPerDay").GetValue(record)).ToString(),
                    IsDrinking = (bool)type.GetProperty("IsDrinking").GetValue(record),
                    DrinkingSinceWhen = (string)type.GetProperty("DrinkingSinceWhen").GetValue(record),
                    NumberOfBottles = ((int?)type.GetProperty("NumberOfBottles").GetValue(record)).ToString(),
                    DrinkingFrequency = (string)type.GetProperty("DrinkingFrequency").GetValue(record),
                    LMP = (string)type.GetProperty("LMP").GetValue(record),
                    LMPType = (string)type.GetProperty("LMPType").GetValue(record),
                    BP1st = (string)type.GetProperty("BP1st").GetValue(record),
                    BP2nd = (string)type.GetProperty("BP2nd").GetValue(record),
                    CardiacRate1st = (string)type.GetProperty("CardiacRate1st").GetValue(record),
                    CardiacRate2nd = (string)type.GetProperty("CardiacRate2nd").GetValue(record),
                    Height = (string)type.GetProperty("Height").GetValue(record),
                    Weight = (string)type.GetProperty("Weight").GetValue(record),
                    BMICategory = (string)type.GetProperty("BMICategory").GetValue(record),
                    VARightEyeWGlasses = (string)type.GetProperty("VARightEyeWGlasses").GetValue(record),
                    VARightEyeWOGlasses = (string)type.GetProperty("VARightEyeWOGlasses").GetValue(record),
                    VALeftEyeWGlasses = (string)type.GetProperty("VALeftEyeWGlasses").GetValue(record),
                    VALeftEyeWOGlasses = (string)type.GetProperty("VALeftEyeWOGlasses").GetValue(record),
                    VisualAcuity = (string)type.GetProperty("VisualAcuity").GetValue(record),
                    Skin = (string)type.GetProperty("Skin").GetValue(record),
                    HeadScalp = (string)type.GetProperty("HeadScalp").GetValue(record),
                    Eyes = (string)type.GetProperty("Eyes").GetValue(record),
                    Ears = (string)type.GetProperty("Ears").GetValue(record),
                    Nose = (string)type.GetProperty("Nose").GetValue(record),
                    TeethTonsilsThroatPharynx = (string)type.GetProperty("TeethTonsilsThroatPharynx").GetValue(record),
                    NeckLymphNodesThyroid = (string)type.GetProperty("NeckLymphNodesThyroid").GetValue(record),
                    ThoraxBreast = (string)type.GetProperty("ThoraxBreast").GetValue(record),
                    HeartLungs = (string)type.GetProperty("HeartLungs").GetValue(record),
                    AbdomenLiverSpleen = (string)type.GetProperty("AbdomenLiverSpleen").GetValue(record),
                    InguinalAreaGenitalsAnus = (string)type.GetProperty("InguinalAreaGenitalsAnus").GetValue(record),
                    ExtremetiesSpine = (string)type.GetProperty("ExtremetiesSpine").GetValue(record),
                    Tattoo = (string)type.GetProperty("Tattoo").GetValue(record),
                    MassCyst = (string)type.GetProperty("MassCyst").GetValue(record),
                    OthersPE = (string)type.GetProperty("OthersPE").GetValue(record),
                    Findings = (string)type.GetProperty("Findings").GetValue(record),
                    VitalSignsBy = (string)type.GetProperty("VitalSignsBy").GetValue(record),
                    HeightWeightBy = (string)type.GetProperty("HeightWeightBy").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(MER))
            {
                reportObject = new
                {
                    DateInputted = GetDataFromRecord<MER, string>(record, "DateInputted", "MM/dd/yyyy"),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    ContactNo = (string)type.GetProperty("ContactNo").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Gender = (string)type.GetProperty("Gender").GetValue(record),
                    CivilStatus = (string)type.GetProperty("CivilStatus").GetValue(record),
                    CompanyName = (string)type.GetProperty("CompanyName").GetValue(record),
                    ChestXray = (string)type.GetProperty("ChestXray").GetValue(record),
                    ChestXrayRemarks = (string)type.GetProperty("ChestXrayRemarks").GetValue(record),
                    CBC = (string)type.GetProperty("CBC").GetValue(record),
                    CBCRemarks = (string)type.GetProperty("CBCRemarks").GetValue(record),
                    Urinalysis = (string)type.GetProperty("Urinalysis").GetValue(record),
                    UrinalysisRemarks = (string)type.GetProperty("UrinalysisRemarks").GetValue(record),
                    Fecalysis = (string)type.GetProperty("Fecalysis").GetValue(record),
                    FecalysisRemarks = (string)type.GetProperty("FecalysisRemarks").GetValue(record),
                    HBsAg = (string)type.GetProperty("HBsAg").GetValue(record),
                    HBsAgRemarks = (string)type.GetProperty("HBsAgRemarks").GetValue(record),
                    DrugTest2Panel = (string)type.GetProperty("DrugTest2Panel").GetValue(record),
                    DrugTest2PanelRemarks = (string)type.GetProperty("DrugTest2PanelRemarks").GetValue(record),
                    DrugTest4Panel = (string)type.GetProperty("DrugTest4Panel").GetValue(record),
                    DrugTest4PanelRemarks = (string)type.GetProperty("DrugTest4PanelRemarks").GetValue(record),
                    Classification = (string)type.GetProperty("Classification").GetValue(record),
                    MedicalSurgicalHistory = (string)type.GetProperty("MedicalSurgicalHistory").GetValue(record),
                    Assessment = (string)type.GetProperty("Assessment").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    AssessmentDoneBy = (string)type.GetProperty("AssessmentDoneBy").GetValue(record),
                    PhysicianName = (string)type.GetProperty("PhysicianName").GetValue(record),
                    PhysicianLicense = (string)type.GetProperty("PhysicianLicense").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(ClinicalChemistry))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<ClinicalChemistry, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Photo = (byte[])type.GetProperty("Photo").GetValue(record),
                    FBSNValue = (string)type.GetProperty("FBSNValue").GetValue(record),
                    FBSResult = (string)type.GetProperty("FBSResult").GetValue(record),
                    TotalCholesterolNValue = (string)type.GetProperty("TotalCholesterolNValue").GetValue(record),
                    TotalCholesterolResult = (string)type.GetProperty("TotalCholesterolResult").GetValue(record),
                    TriglyceridesNValue = (string)type.GetProperty("TriglyceridesNValue").GetValue(record),
                    TriglyceridesResult = (string)type.GetProperty("TriglyceridesResult").GetValue(record),
                    HDLNValue = (string)type.GetProperty("HDLNValue").GetValue(record),
                    HDLResult = (string)type.GetProperty("HDLResult").GetValue(record),
                    BUNNValue = (string)type.GetProperty("BUNNValue").GetValue(record),
                    BUNResult = (string)type.GetProperty("BUNResult").GetValue(record),
                    CreatinineNValue = (string)type.GetProperty("CreatinineNValue").GetValue(record),
                    CreatinineResult = (string)type.GetProperty("CreatinineResult").GetValue(record),
                    BloodUricAcidNValue = (string)type.GetProperty("BloodUricAcidNValue").GetValue(record),
                    BloodUricAcidResult = (string)type.GetProperty("BloodUricAcidResult").GetValue(record),
                    LDLNValue = (string)type.GetProperty("LDLNValue").GetValue(record),
                    LDLResult = (string)type.GetProperty("LDLResult").GetValue(record),
                    SGPTNValue = (string)type.GetProperty("SGPTNValue").GetValue(record),
                    SGPTResult = (string)type.GetProperty("SGPTResult").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(Hematology))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<Hematology, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Photo = (byte[])type.GetProperty("Photo").GetValue(record),
                    HematocritNValue = (string)type.GetProperty("HematocritNValue").GetValue(record),
                    HematocritResult = (string)type.GetProperty("HematocritResult").GetValue(record),
                    HemoglobinNValue = (string)type.GetProperty("HemoglobinNValue").GetValue(record),
                    HemoglobinResult = (string)type.GetProperty("HemoglobinResult").GetValue(record),
                    WBCCountNValue = (string)type.GetProperty("WBCCountNValue").GetValue(record),
                    WBCCountResult = (string)type.GetProperty("WBCCountResult").GetValue(record),
                    SegmentersNValue = (string)type.GetProperty("SegmentersNValue").GetValue(record),
                    SegmentersResult = (string)type.GetProperty("SegmentersResult").GetValue(record),
                    LymphocytesNValue = (string)type.GetProperty("LymphocytesNValue").GetValue(record),
                    LymphocytesResult = (string)type.GetProperty("LymphocytesResult").GetValue(record),
                    EosinophilsNValue = (string)type.GetProperty("EosinophilsNValue").GetValue(record),
                    EosinophilsResult = (string)type.GetProperty("EosinophilsResult").GetValue(record),
                    MonocytesNValue = (string)type.GetProperty("MonocytesNValue").GetValue(record),
                    MonocytesResult = (string)type.GetProperty("MonocytesResult").GetValue(record),
                    BasophilsNValue = (string)type.GetProperty("BasophilsNValue").GetValue(record),
                    BasophilsResult = (string)type.GetProperty("BasophilsResult").GetValue(record),
                    StabNValue = (string)type.GetProperty("StabNValue").GetValue(record),
                    StabResult = (string)type.GetProperty("StabResult").GetValue(record),
                    PlateletCountNValue = (string)type.GetProperty("PlateletCountNValue").GetValue(record),
                    PlateletCountResult = (string)type.GetProperty("PlateletCountResult").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(ClinicalChemistry1))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<ClinicalChemistry1, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Photo = (byte[])type.GetProperty("Photo").GetValue(record),
                    Test = (string)type.GetProperty("Test").GetValue(record),
                    Result = (string)type.GetProperty("Result").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(ClinicalChemistry2))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<ClinicalChemistry2, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Photo = (byte[])type.GetProperty("Photo").GetValue(record),
                    AlkalinePhosphataseCNValue = (string)type.GetProperty("AlkalinePhosphataseCNValue").GetValue(record),
                    AlkalinePhosphataseCUnit = (string)type.GetProperty("AlkalinePhosphataseCUnit").GetValue(record),
                    AlkalinePhosphataseCResults = (string)type.GetProperty("AlkalinePhosphataseCResults").GetValue(record),
                    AlkalinePhosphataseSNValue = (string)type.GetProperty("AlkalinePhosphataseSNValue").GetValue(record),
                    AlkalinePhosphataseSUnit = (string)type.GetProperty("AlkalinePhosphataseSUnit").GetValue(record),
                    AlkalinePhosphataseSResults = (string)type.GetProperty("AlkalinePhosphataseSResults").GetValue(record),
                    SGOTCNValue = (string)type.GetProperty("SGOTCNValue").GetValue(record),
                    SGOTCUnit = (string)type.GetProperty("SGOTCUnit").GetValue(record),
                    SGOTCResults = (string)type.GetProperty("SGOTCResults").GetValue(record),
                    SGOTSNValue = (string)type.GetProperty("SGOTSNValue").GetValue(record),
                    SGOTSUnit = (string)type.GetProperty("SGOTSUnit").GetValue(record),
                    SGOTSResults = (string)type.GetProperty("SGOTSResults").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(Immunology))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<Immunology, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Photo = (byte[])type.GetProperty("Photo").GetValue(record),
                    Test = (string)type.GetProperty("Test").GetValue(record),
                    Result = (string)type.GetProperty("Result").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(PregnancyTest))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<PregnancyTest, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Photo = (byte[])type.GetProperty("Photo").GetValue(record),
                    Result = (string)type.GetProperty("Result").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(Serology))
            {
                reportObject = new
                {
                    PatientCode = (string)type.GetProperty("PatientCode").GetValue(record),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyOrPhysician = (string)type.GetProperty("CompanyOrPhysician").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    Sex = (string)type.GetProperty("Sex").GetValue(record),
                    DateRequested = GetDataFromRecord<Serology, string>(record, "DateRequested", "MM/dd/yyyy"),
                    Photo = (byte[])type.GetProperty("Photo").GetValue(record),
                    Test = (string)type.GetProperty("Test").GetValue(record),
                    Result = (string)type.GetProperty("Result").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            return reportObject;
        }

        private static T2 GetDataFromRecord<T, T2>(object record, string field, string dateFormat = null)
        {
            try
            {
                Type type = typeof(T);
                Type outType = typeof(T2);
                var value = type.GetProperty(field).GetValue(record);

                if (value == null)
                {
                    return default(T2);
                }
                else
                {
                    Type propType = value.GetType();

                    if (propType == typeof(DateTime))
                    {
                        if (dateFormat != null)
                            return (T2)Convert.ChangeType(((DateTime)type.GetProperty(field).GetValue(record)).ToString(dateFormat), typeof(T2));
                        else
                            return (T2)Convert.ChangeType(((DateTime)type.GetProperty(field).GetValue(record)), typeof(T2));
                    }
                    else
                    {
                        if (value == null)
                            return (T2)Convert.ChangeType(string.Empty, typeof(T2));
                        else
                            return (T2)value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
