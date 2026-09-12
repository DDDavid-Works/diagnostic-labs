namespace DiagnosticLabs.Models.Reports
{
    public class ClinicalChemistry2Report
    {
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string CompanyOrPhysician { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string DateRequested { get; set; }
        public byte[] Photo { get; set; }
        public string AlkalinePhosphataseCNValue { get; set; }
        public string AlkalinePhosphataseCUnit { get; set; }
        public string AlkalinePhosphataseCResults { get; set; }
        public string AlkalinePhosphataseSNValue { get; set; }
        public string AlkalinePhosphataseSUnit { get; set; }
        public string AlkalinePhosphataseSResults { get; set; }
        public string SGOTCNValue { get; set; }
        public string SGOTCUnit { get; set; }
        public string SGOTCResults { get; set; }
        public string SGOTSNValue { get; set; }
        public string SGOTSUnit { get; set; }
        public string SGOTSResults { get; set; }
        public string MedicalTechnologist { get; set; }
        public string Pathologist { get; set; }
        public byte[] CompanySetupLogo { get; set; }
    }
}
