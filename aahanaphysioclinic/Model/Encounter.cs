
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
        public int VAScale { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public DateTime? EncounterDate { get; set; }

        // For Encouter Date Selection
        [NotMapped]
        public int EncounterDateTimeDay { get; set; }
        [NotMapped]
        public int EncounterDateTimeMonth { get; set; }
        [NotMapped]
        public int EncounterDateTimeYear { get; set; }

        // For Encounter From Time Selection
        [NotMapped]
        public int FromHour { get; set; }
        [NotMapped]
        public int FromMinute { get; set; }
        [NotMapped]
        public string FromMeridiem { get; set; }

        // For Encounter To Time Selection
        [NotMapped]
        public int? ToHour { get; set; }
        [NotMapped]
        public int? ToMinute { get; set; }
        [NotMapped]
        public string? ToMeridiem { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        [NotMapped]
        public List<string>? MedicalHistory { get; set; } = new List<string>();
        public string? ApplicationUserId { get; set; }
        public int? PatientId { get; set; }

        public Encounter()
        {
            // Set EncounterDate from corresponding non-mapped properties
            if (EncounterDateTimeDay != 0 && EncounterDateTimeMonth != 0 && EncounterDateTimeYear != 0)
            {
                EncounterDate = new DateTime(EncounterDateTimeYear, EncounterDateTimeMonth, EncounterDateTimeDay);
            }

            // Set From property from related non-mapped properties
            if (FromHour != 0 && FromMinute != 0 && FromMeridiem != null)
            {
                From = DateTime.Today
                    .AddHours(FromMeridiem == "PM" && FromHour != 12 ? FromHour + 12 : FromHour) // Adjust for 12 PM
                    .AddMinutes(FromMinute);
            }

            // Set To property with corresponding non-mapped properties
            if (ToHour.HasValue && ToMinute.HasValue && ToMeridiem != null)
            {
                To = DateTime.Today
                    .AddHours(ToMeridiem == "PM" && ToHour != 12 ? ToHour.Value + 12 : ToHour.Value) // Adjust for 12 PM
                    .AddMinutes(ToMinute.Value);
            }
        }


    }
}
