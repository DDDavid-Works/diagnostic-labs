namespace DiagnosticLabs.Models.Reports
{
    public class HematologyReport
    {
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string CompanyOrPhysician { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string DateRequested { get; set; }
        public byte[] Photo { get; set; }
        public string HematocritNValue { get; set; }
        public string HematocritResult { get; set; }
        public string HemoglobinNValue { get; set; }
        public string HemoglobinResult { get; set; }
        public string WBCCountNValue { get; set; }
        public string WBCCountResult { get; set; }
        public string SegmentersNValue { get; set; }
        public string SegmentersResult { get; set; }
        public string LymphocytesNValue { get; set; }
        public string LymphocytesResult { get; set; }
        public string EosinophilsNValue { get; set; }
        public string EosinophilsResult { get; set; }
        public string MonocytesNValue { get; set; }
        public string MonocytesResult { get; set; }
        public string BasophilsNValue { get; set; }
        public string BasophilsResult { get; set; }
        public string StabNValue { get; set; }
        public string StabResult { get; set; }
        public string PlateletCountNValue { get; set; }
        public string PlateletCountResult { get; set; }
        public string Remarks { get; set; }
        public string MedicalTechnologist { get; set; }
        public string Pathologist { get; set; }
        public byte[] CompanySetupLogo { get; set; }
    }
}
