using System.ComponentModel.DataAnnotations;

namespace aahanaphysioclinic.Model
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
