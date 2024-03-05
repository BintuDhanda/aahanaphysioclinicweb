using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aahanaphysioclinic.Model
{
    public class LabReport
    {
        [Key]
        public int Id { get; set; }
        public DateTime? UploadedOn { get; set; }
        [NotMapped]
        public IFormFile? LabReportFile { get; set; }
        public int? FileId { get; set; }
        public string? LabReportType { get; set; }
        public int EncounterId { get; set; }
    }
}
