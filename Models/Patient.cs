
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class Patient : Base<int>
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public int VisitBalance { get; set; }
        public string? Gender { get; set; }
        public string? Occupation { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? City  { get; set; }
        public string? PinCode  { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser User { get; set; }
        public bool IsDeleted { get; set; }
    }
}
