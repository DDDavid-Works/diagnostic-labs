using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models.Views
{
    public class PatientCompany
    {
        [Key]
        public long PatientId { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
