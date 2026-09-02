namespace DiagnosticLabs.Models.Reports
{
    public class MedicalExaminationReport
    {
        public string DateInputted { get; set; }
        public string PatientName { get; set; }
        public string ContactNo { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string CompanyName { get; set; }
        public string ChestXray { get; set; }
        public string ChestXrayRemarks { get; set; }
        public string CBC { get; set; }
        public string CBCRemarks { get; set; }
        public string Urinalysis { get; set; }
        public string UrinalysisRemarks { get; set; }
        public string Fecalysis { get; set; }
        public string FecalysisRemarks { get; set; }
        public string HBsAg { get; set; }
        public string HBsAgRemarks { get; set; }
        public string DrugTest2Panel { get; set; }
        public string DrugTest2PanelRemarks { get; set; }
        public string DrugTest4Panel { get; set; }
        public string DrugTest4PanelRemarks { get; set; }
        public string Classification { get; set; }
        public string MedicalSurgicalHistory { get; set; }
        public string Assessment { get; set; }
        public string Remarks { get; set; }
        public string AssessmentDoneBy { get; set; }
        public string PhysicianName { get; set; }
        public string PhysicianLicense { get; set; }
        public byte[] CompanySetupLogo { get; set; }

    }
}
