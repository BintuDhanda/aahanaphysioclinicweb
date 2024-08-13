using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class LabReport
    {
        public LabReport()
        {
            Timestamp = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public int FileId { get; set; }
        [ForeignKey(nameof(FileId))]
        public FileStorage File { get; set; }
        public int EncounterId { get; set; }
        [ForeignKey(nameof(EncounterId))]
        public Encounter Encounter { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
