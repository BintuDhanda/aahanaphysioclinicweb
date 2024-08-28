using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class Payment : Base<int>
    {
        public int PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }
        public string Mode { get; set; }
        public string? TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int Visits { get; set; }
    }
}
