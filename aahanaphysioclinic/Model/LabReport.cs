namespace aahanaphysioclinic.Model
{
    public class LabReport
    {
        public int Id { get; set; }
        public DateTime UploadedOn { get; set; }
        public string? ReportUrl { get; set; }
        public string? LabReportType { get; set; }
        public int EncounterId { get; set; }
    }
}
