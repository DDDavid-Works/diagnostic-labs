namespace DiagnosticLabs.Models.Reports
{
    public class UrinalysisReport
    {
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string CompanyOrPhysician { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string DateRequested { get; set; }
        public byte[] Photo { get; set; }
        public string Color { get; set; }
        public string Appearance { get; set; }
        public string Reaction { get; set; }
        public string SPGravity { get; set; }
        public string Albumin { get; set; }
        public string Sugar { get; set; }
        public string PusCells { get; set; }
        public string RedCells { get; set; }
        public string MucusThreads { get; set; }
        public string EpithelialCells { get; set; }
        public string AmorphousUratesPO4 { get; set; }
        public string Bacteria { get; set; }
        public string Casts { get; set; }
        public string Crystals { get; set; }
        public string Others { get; set; }
        public string Remarks { get; set; }
        public string MedicalTechnologist { get; set; }
        public string Pathologist { get; set; }
        public byte[] CompanySetupLogo { get; set; }
    }
}
