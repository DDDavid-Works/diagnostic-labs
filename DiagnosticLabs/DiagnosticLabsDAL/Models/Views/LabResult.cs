using System;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models.Views
{
    public class LabResult
    {
        [Key]
        public long RowNumber { get; set; }
        public string Service { get; set; }
        public long Id { get; set; }
        public long? PatientRegistrationId { get; set; }
        public long? PatientId { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public long? CompanyId { get; set; }
        public string Company { get; set; }
        public DateTime? DateRequested { get; set; }
        public bool IsActive { get; set; }
    }
}
