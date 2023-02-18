using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiagnosticLabsDAL.Models.Views
{
    public class PatientRegistrationPayment
    {
        [Key]
        public long PatientRegistrationId { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }

        [NotMapped]
        public string PatientRegistrationPaymentAmountDue { get; set; }

        [NotMapped]
        public string PatientRegistrationPaymentAmountPaid { get; set; }

        [NotMapped]
        public string PatientRegistrationPaymentBalance { get; set; }
    }
}
