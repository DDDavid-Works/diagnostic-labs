using System;
using System.ComponentModel.DataAnnotations;

namespace DiagnosticLabsDAL.Models.Views
{
    public class PaymentDetail
    {
        [Key]
        public long PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public long PatientRegistrationId { get; set; }
        public DateTime PatientRegistrationDate { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public long PatientId { get; set; }
        public string PatientName { get; set;}
        public long CompanyId { get; set; }
        public string CompanyName { get; set;}
    }
}
