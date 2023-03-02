using DiagnosticLabsDAL.Models;
using System.Collections.Generic;

namespace DiagnosticLabsBLL.Globals
{
    public class Globals
    {
        public static long LOGGEDINUSERID = 0;
        public static bool ISADMIN = false;
        public static string COMPANYSETUPCODE = string.Empty;
        public static List<UserPermission> USERPERMISSIONS = new List<UserPermission>();
        public static List<MenuItem> MENUITEMS = new List<MenuItem>();

        public static List<Module> MODULES = new List<Module>();
        public static List<ModuleType> MODULETYPES = new List<ModuleType>();
        public static Dictionary<int, string> ModuleIcons = new Dictionary<int, string>();

        public static long PATIENTREGISTRATIONIDTOPAY = 0;
        public static long PATIENTREGISTRATIONIDTOINPUT = 0;
    }

    public enum ListEntry
    {
        Company,
        Batch
    }

    public class Modules
    {
        //PATIENTS
        public const string PatientRegistrations = "Patient Registrations";
        public const string Patients = "Patients";
        public const string Companies = "Companies";

        //SALES
        public const string Payments = "Payment";

        //LAB RESULTS
        public const string StoolFecalysis = "Stool/Fecalysis";
        public const string Urinalysis = "Urinalysis";
        public const string Hematology = "Hematology";
        public const string Immunology = "Immunology";
        public const string Serology = "Serology";
        public const string PregnancyTest = "Pregnancy Test";
        public const string ClinicalChemistry = "Clinical Chemistry";
        public const string ClinicalChemistry1 = "Clinical Chemistry 1";
        public const string ClinicalChemistry2 = "Clinical Chemistry 2";
        public const string MedicalExamination = "Medical Examination";
        public const string MedicalExaminationPage2 = "Medical Examination Page 2";
        public const string AnnualPhysicalExam = "Annual Physical Exam";
        public const string AnnualPhysicalExamPage2 = "Annual Physical Exam Page 2";

        //MANAGEMENT
        public const string Departments = "Departments";
        public const string Services = "Services";
        public const string Packages = "Packages";
        public const string Items = "Items";
        public const string ItemLocations = "ItemLocations";
        public const string Discounts = "Discounts";

        //REPORTS
        public const string SalesReport = "SalesReport";
        public const string StatementOfAccounts = "Statement Of Accounts";

        //SETTINGS
        public const string CompanySetup = "Company Setup";
        public const string Users = "Users";
        public const string ChangePassword = "Change Password";
    }
}
