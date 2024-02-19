namespace aahanaphysioclinic.Model
{
    public class LabReport
    {
        public int Id { get; set; }
        public DateTime UploadTime  { get; set; }
        public string? ReportUrl { get; set; }
        public string? LabReportType { get; set; }

        // Navigation

        public Encounter Encounter { get; set; }
        public int EncounterId { get; set; }
    }
}
