namespace DiagnosticLabs.Models.Reports
{
    public class StoolFecalysisReport
    {
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string CompanyOrPhysician { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string DateRequested { get; set; }
        public byte[] Photo { get; set; }
        public string Color { get; set; }
        public string Consistency { get; set; }
        public string Result { get; set; }
        public string Remarks { get; set; }
        public string MedicalTechnologist { get; set; }
        public string Pathologist { get; set; }
        public byte[] CompanySetupLogo { get; set; }
    }
}
