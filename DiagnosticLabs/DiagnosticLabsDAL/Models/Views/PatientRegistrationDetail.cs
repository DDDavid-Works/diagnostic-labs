using System;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models.Views
{
    public class PatientRegistrationDetail
    {
        [Key]
        public long PatientRegistrationId { get; set; }
        public DateTime InputDate { get; set; }
        public decimal Price { get; set; }
        public long PatientId { get; set; }
        public string PatientName { get; set; }
        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
