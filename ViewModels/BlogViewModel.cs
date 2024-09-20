using AahanaClinic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.ViewModels
{
    public class BlogViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
