using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models.Views
{
    public class PatientRegistrationDetail
    {
        [Key]
        public long PatientRegistrationId { get; set; }
        public DateTime InputDate { get; set; }
        public decimal AmountDue { get; set; }
        public long PatientId { get; set; }
        public string PatientName { get; set; }
        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }

        [NotMapped]
        public List<PatientRegistrationService> PatientRegistrationServices { get; set; }
    }
}
