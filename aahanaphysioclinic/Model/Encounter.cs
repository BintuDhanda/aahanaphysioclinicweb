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
        
        // For Encouter Date Selection
        [NotMapped]
        public int EncounterDateTimeDay { get; set; }
        [NotMapped]
        public int EncounterDateTimeMonth { get; set; }
        [NotMapped]
        public int EncounterDateTimeYear { get; set; }

        public DateTime? EncounterDate
        {
            get
            {
                return new DateTime(EncounterDateTimeYear, EncounterDateTimeMonth, EncounterDateTimeDay, 0, 0, 0, 0);
            }
        }


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

        public DateTime From {
            get
            {
                int hour = FromHour;
                if (FromMeridiem == "PM" && hour < 12)
                {
                    hour += 12;
                }
                else if (FromMeridiem == "AM" && hour == 12)
                {
                    hour = 0;
                }

                return new DateTime(0, 0, 0, hour, FromMinute, 0);
            }
        }
        public DateTime To {
            get
            {
                int hour = ToHour;
                if(ToMeridiem=="PM" && hour < 12)
                {
                    hour += 12;
                }
                else if(ToMeridiem=="AM" && hour == 12)
                {
                    hour = 0;
                }
                return new DateTime(0, 0, 0, hour, ToMinute, 0);
            }
            }

        [NotMapped]
        public List<string>? MedicalHistory { get; set; } = new List<string>();
        public string? ApplicationUserId { get; set; }
        public int? PatientId { get; set; }

    }
}
