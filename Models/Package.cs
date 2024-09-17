using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class Package : Base<int>
    {
        public int PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }
        public int ModeId { get; set; }
        [ForeignKey(nameof(ModeId))]
        public PaymentMode Mode { get; set; }
        public string? TransactionId { get; set; }
        public decimal Amount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public DateTime Date { get; set; }
        public int Visits { get; set; }
        public decimal AveragePrice { get; set; }
        public int VisitBalance { get; set; }
    }
}
