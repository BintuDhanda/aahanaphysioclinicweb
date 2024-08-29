using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class LabReport : Base<int>
    {
        public int FileId { get; set; }
        [ForeignKey(nameof(FileId))]
        public FileStorage File { get; set; }
        public int PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }
        public string Type { get; set; }
    }
}
