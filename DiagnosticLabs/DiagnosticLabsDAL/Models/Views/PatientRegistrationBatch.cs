using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models.Views
{
    public class PatientRegistrationBatch
    {
        [Key]
        public long CompanyId { get; set; }
        public string BatchName { get; set; }
    }
}
