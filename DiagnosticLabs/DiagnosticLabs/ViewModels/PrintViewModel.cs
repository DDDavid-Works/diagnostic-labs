using CrystalDecisions.CrystalReports.Engine;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
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
                    //((DateTime)type.GetProperty("DateInputted").GetValue(record)).ToString("MM/dd/yyyy"), 
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyName = (string)type.GetProperty("CompanyName").GetValue(record),
                    DepartmentOrAgency = (string)type.GetProperty("DepartmentOrAgency").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    BirthDate = GetDataFromRecord<APE, string>(record, "BirthDate", "MM/dd/yyyy"),
                    //((DateTime)type.GetProperty("BirthDate").GetValue(record)).ToString("MM/dd/yyyy"),
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
