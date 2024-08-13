using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class Encounter
    {
        public Encounter()
        {
            Timestamp = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }
        public decimal? Fees { get; set; }
        public string? CheifComplaint { get; set; }
        public byte VAScale { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public DateTime EncounterDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public string? MedicalHistory { get; set; }
    }
}
