namespace AahanaClinic.Models
{
    public class Seo : Base<int>
    {
        public string Page { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
    }
}
