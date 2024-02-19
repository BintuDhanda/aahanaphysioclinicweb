
using System.ComponentModel.DataAnnotations;

namespace aahanaphysioclinic.Model
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
