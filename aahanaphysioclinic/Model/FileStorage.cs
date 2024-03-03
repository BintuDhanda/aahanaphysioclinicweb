using System.ComponentModel.DataAnnotations;

namespace aahanaphysioclinic.Model
{
    public class FileStorage
    {
        [Key]
        public int FileId { get; set; }
        public string? FileName { get; set; }
        public string? FileURL { get; set;}
    }
}
