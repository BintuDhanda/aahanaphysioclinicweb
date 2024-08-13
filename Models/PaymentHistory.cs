using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class PaymentHistory
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }
        public string? TransactionId { get; set; }
        public string Mode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
