using System.ComponentModel.DataAnnotations;

namespace AahanaClinic.Models
{
    public class FileStorage
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set;}
    }
}
