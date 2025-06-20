﻿using CrystalDecisions.CrystalReports.Engine;
using DiagnosticLabs.ViewModels.Base;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using System;
using System.Drawing.Imaging;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Media.Media3D;
using CrystalDecisions.CrystalReports.ViewerObjectModel;

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
                    DateRequested = ((DateTime)type.GetProperty("DateRequested").GetValue(record)).ToString("MM/dd/yyyy"),
                    Color = (string)type.GetProperty("Color").GetValue(record),
                    Consistency = (string)type.GetProperty("Consistency").GetValue(record),
                    Result = (string)type.GetProperty("Result").GetValue(record),
                    Remarks = (string)type.GetProperty("Remarks").GetValue(record),
                    MedicalTechnologist = (string)type.GetProperty("MedicalTechnologist").GetValue(record),
                    Pathologist = (string)type.GetProperty("Pathologist").GetValue(record),
                    CompanySetupLogo = companySetup.Logo
                };
            }
            else if (typeof(T) == typeof(APE))
            {
                string DateInputted2 = ((DateTime)type.GetProperty("DateInputted").GetValue(record)).ToString("MM/dd/yyyy");

                string DateInputted = GetDataFromRecord<string>(record, "DateInputted", "MM/dd/yyyy");
                string PatientName = GetDataFromRecord<string>(record, "PatientName");

                string CompanyName = (string)type.GetProperty("CompanyName").GetValue(record);
                string DepartmentOrAgency = (string)type.GetProperty("DepartmentOrAgency").GetValue(record);
                string Age = (string)type.GetProperty("Age").GetValue(record);
                string BirthDate = ((DateTime)type.GetProperty("BirthDate").GetValue(record)).ToString("MM/dd/yyyy");
                string Gender = (string)type.GetProperty("Gender").GetValue(record);
                string CivilStatus = (string)type.GetProperty("CivilStatus").GetValue(record);
                string ContactNo = (string)type.GetProperty("ContactNo").GetValue(record);
                string ENT = (string)type.GetProperty("ENT").GetValue(record);
                string Gastroenterology = (string)type.GetProperty("Gastroenterology").GetValue(record);
                string Respiratory = (string)type.GetProperty("Respiratory").GetValue(record);
                string IntegumentarySkin = (string)type.GetProperty("IntegumentarySkin").GetValue(record);
                string Cardiology = (string)type.GetProperty("Cardiology").GetValue(record);
                string Psychology = (string)type.GetProperty("Psychology").GetValue(record);
                string Endocrinology = (string)type.GetProperty("Endocrinology").GetValue(record);
                string OBGyneUrology = (string)type.GetProperty("OBGyneUrology").GetValue(record);
                string Muscoloskeletal = (string)type.GetProperty("Muscoloskeletal").GetValue(record);
                string InfectiousCommunicable = (string)type.GetProperty("InfectiousCommunicable").GetValue(record);
                string Neurological = (string)type.GetProperty("Neurological").GetValue(record);
                string Surgical = (string)type.GetProperty("Surgical").GetValue(record);
                string OthersPast = (string)type.GetProperty("OthersPast").GetValue(record);
                string Medications = (string)type.GetProperty("Medications").GetValue(record);
                string ReviewOfSystems = (string)type.GetProperty("ReviewOfSystems").GetValue(record);
                string Allergies = (string)type.GetProperty("Allergies").GetValue(record);
                bool IsSmoking = (bool)type.GetProperty("IsSmoking").GetValue(record);
                string SmokingSinceWhen = (string)type.GetProperty("SmokingSinceWhen").GetValue(record);
                string NumberOfSticksPerDay = (string)type.GetProperty("NumberOfSticksPerDay").GetValue(record);
                bool IsDrinking = (bool)type.GetProperty("IsDrinking").GetValue(record);
                string DrinkingSinceWhen = (string)type.GetProperty("DrinkingSinceWhen").GetValue(record);
                string NumberOfBottles = (string)type.GetProperty("NumberOfBottles").GetValue(record);
                string DrinkingFrequency = (string)type.GetProperty("DrinkingFrequency").GetValue(record);
                string LMP = (string)type.GetProperty("LMP").GetValue(record);
                string LMPType = (string)type.GetProperty("LMPType").GetValue(record);
                string BP1st = (string)type.GetProperty("BP1st").GetValue(record);
                string BP2nd = (string)type.GetProperty("BP2nd").GetValue(record);
                string CardiacRate1st = (string)type.GetProperty("CardiacRate1st").GetValue(record);
                string CardiacRate2nd = (string)type.GetProperty("CardiacRate2nd").GetValue(record);
                string Height = (string)type.GetProperty("Height").GetValue(record);
                string Weight = (string)type.GetProperty("Weight").GetValue(record);
                string BMICategory = (string)type.GetProperty("BMICategory").GetValue(record);
                string VARightEyeWGlasses = (string)type.GetProperty("VARightEyeWGlasses").GetValue(record);
                string VARightEyeWOGlasses = (string)type.GetProperty("VARightEyeWOGlasses").GetValue(record);
                string VALeftEyeWGlasses = (string)type.GetProperty("VALeftEyeWGlasses").GetValue(record);
                string VALeftEyeWOGlasses = (string)type.GetProperty("VALeftEyeWOGlasses").GetValue(record);
                string VisualAcuity = (string)type.GetProperty("VisualAcuity").GetValue(record);
                string Skin = (string)type.GetProperty("Skin").GetValue(record);
                string HeadScalp = (string)type.GetProperty("HeadScalp").GetValue(record);
                string Eyes = (string)type.GetProperty("Eyes").GetValue(record);
                string Ears = (string)type.GetProperty("Ears").GetValue(record);
                string Nose = (string)type.GetProperty("Nose").GetValue(record);
                string TeethTonsilsThroatPharynx = (string)type.GetProperty("TeethTonsilsThroatPharynx").GetValue(record);
                string NeckLymphNodesThyroid = (string)type.GetProperty("NeckLymphNodesThyroid").GetValue(record);
                string ThoraxBreast = (string)type.GetProperty("ThoraxBreast").GetValue(record);
                string HeartLungs = (string)type.GetProperty("HeartLungs").GetValue(record);
                string AbdomenLiverSpleen = (string)type.GetProperty("AbdomenLiverSpleen").GetValue(record);
                string InguinalAreaGenitalsAnus = (string)type.GetProperty("InguinalAreaGenitalsAnus").GetValue(record);
                string ExtremetiesSpine = (string)type.GetProperty("ExtremetiesSpine").GetValue(record);
                string Tattoo = (string)type.GetProperty("Tattoo").GetValue(record);
                string MassCyst = (string)type.GetProperty("MassCyst").GetValue(record);
                string OthersPE = (string)type.GetProperty("OthersPE").GetValue(record);
                string Findings = (string)type.GetProperty("Findings").GetValue(record);
                string VitalSignsBy = (string)type.GetProperty("VitalSignsBy").GetValue(record);
                string HeightWeightBy = (string)type.GetProperty("HeightWeightBy").GetValue(record);
                //string CompanySetupLogo = companySetup.Logo;

                reportObject = new
                {
                    DateInputted = ((DateTime)type.GetProperty("DateInputted").GetValue(record)).ToString("MM/dd/yyyy"),
                    PatientName = (string)type.GetProperty("PatientName").GetValue(record),
                    CompanyName = (string)type.GetProperty("CompanyName").GetValue(record),
                    DepartmentOrAgency = (string)type.GetProperty("DepartmentOrAgency").GetValue(record),
                    Age = (string)type.GetProperty("Age").GetValue(record),
                    BirthDate = ((DateTime)type.GetProperty("BirthDate").GetValue(record)).ToString("MM/dd/yyyy"),
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
                    NumberOfSticksPerDay = (string)type.GetProperty("NumberOfSticksPerDay").GetValue(record),
                    IsDrinking = (bool)type.GetProperty("IsDrinking").GetValue(record),
                    DrinkingSinceWhen = (string)type.GetProperty("DrinkingSinceWhen").GetValue(record),
                    NumberOfBottles = (string)type.GetProperty("NumberOfBottles").GetValue(record),
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

        private static T GetDataFromRecord<T>(object record, string field, string dateFormat = null)
        {
            try
            {
                Type type = typeof(T);
                var value = type.GetProperty(field).GetValue(record);

                if (dateFormat == null)
                    return (T)value;
                else
                {
                    if (value != null)
                        return (T)Convert.ChangeType(((DateTime)value).ToString("MM/dd/yyyy"), typeof(T));
                    else
                        return (T)Convert.ChangeType(string.Empty, typeof(T));
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
