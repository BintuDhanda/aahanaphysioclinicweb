using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aahanaphysioclinic.Model
{
    public class Encounter
    {
        [Key]
        public int EncounterId { get; set; }
        public decimal? Fees { get; set; }
        public string? CheifComplaint { get; set; }
        public byte VAScale { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        
        // For Encouter Date Selection
        [NotMapped]
        public int EncounterDateTimeDay { get; set; }
        [NotMapped]
        public int EncounterDateTimeMonth { get; set; }
        [NotMapped]
        public int EncounterDateTimeYear { get; set; }
        public DateTime? EncounterDate { get; set; }

        // For Encounter From Time Selection
        [NotMapped]
        public int FromHour { get; set; } 
        [NotMapped]
        public int FromMinute { get; set; }
        [NotMapped]
        public string FromMeridiem { get; set; }

        // For Encounter To Time Selection
        [NotMapped]
        public int ToHour { get; set; }
        [NotMapped]
        public int ToMinute { get; set; }
        [NotMapped]
        public string ToMeridiem { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string? MedicalHistory { get; set; }
        [NotMapped]
        public List<string>? MedicalHistoryItems { get; set; } = new List<string>();
        public string? ApplicationUserId { get; set; }
        public int? PatientId { get; set; }
        
        [NotMapped]
        public string? PatientName { get; set; }

    }
}
