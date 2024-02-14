using System.ComponentModel.DataAnnotations;

namespace aahanaphysioclinic.Model
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        public string? PatientName { get; set; }

        [MaxLength(15)]
        public string? MobileNumber { get; set; }

        public DateTime CreatedOn { get; set; } = System.DateTime.UtcNow;

        // Navigation property
        public string? ApplicationUserId { get; set; }  // Foreign key
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
