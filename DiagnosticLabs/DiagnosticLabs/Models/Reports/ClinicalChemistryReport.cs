namespace DiagnosticLabs.Models.Reports
{
    public class ClinicalChemistryReport
    {
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string CompanyOrPhysician { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string DateRequested { get; set; }
        public byte[] Photo { get; set; }
        public string FBSNValue { get; set; }
        public string FBSResult { get; set; }
        public string TotalCholesterolNValue { get; set; }
        public string TotalCholesterolResult { get; set; }
        public string TriglyceridesNValue { get; set; }
        public string TriglyceridesResult { get; set; }
        public string HDLNValue { get; set; }
        public string HDLResult { get; set; }
        public string BUNNValue { get; set; }
        public string BUNResult { get; set; }
        public string CreatinineNValue { get; set; }
        public string CreatinineResult { get; set; }
        public string BloodUricAcidNValue { get; set; }
        public string BloodUricAcidResult { get; set; }
        public string LDLNValue { get; set; }
        public string LDLResult { get; set; }
        public string SGPTNValue { get; set; }
        public string SGPTResult { get; set; }
        public string MedicalTechnologist { get; set; }
        public string Pathologist { get; set; }
        public byte[] CompanySetupLogo { get; set; }
    }
}
