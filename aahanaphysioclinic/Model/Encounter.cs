using System.ComponentModel.DataAnnotations;

namespace aahanaphysioclinic.Model
{
    public class Encounter
    {
        [Key]
        public int Id { get; set; }
        public string? EncounterName { get; set; }
        public decimal Fees { get; set; }
        public string? ExpectedDiagnosis { get; set; }

        // Navigation
        public ApplicationUser? ApplicationUser { get; set; }
        public string? ApplicationUserId { get; set; }

        public Patient? Patient { get; set; }
        public int PatientId { get;}

    }
}
