namespace AahanaClinic.ViewModels
{
    public class ReportViewModel
    {
        public int? Id { get; set; }
        public int PatientId { get; set; }
        public string Type { get; set; }
        public IFormFile File { get; set; }
    }
}
