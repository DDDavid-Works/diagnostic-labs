using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer;
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
                default:
                    break;
            }

            if (record != null)
            {
                this.ReportDocument.SetDataSource((new[] { record }).ToList());

                switch (module)
                {
                    case Modules.StoolFecalysis:
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
                reportObject = new {
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

            return reportObject;
        }
        #endregion
    }
}
