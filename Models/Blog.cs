namespace AahanaClinic.Models
{
    public class Blog : Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
    }
}
