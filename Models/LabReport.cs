using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class LabReport : Base<int>
    {
        public int FileId { get; set; }
        [ForeignKey(nameof(FileId))]
        public FileStorage File { get; set; }
        public int EncounterId { get; set; }
        [ForeignKey(nameof(EncounterId))]
        public Encounter Encounter { get; set; }
        public string Type { get; set; }
    }
}
