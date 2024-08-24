namespace AahanaClinic.ViewModels
{
    public class SettingsViewModel
    {
        public int Id { get; set; }
        public IFormFile? Logo { get; set; }
        public string? LogoUrl { get; set; }
        public string Address { get; set; }
        public IFormFile? Signature { get; set; }
        public string? SignatureUrl { get; set; }
    }
}
