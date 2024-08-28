namespace AahanaClinic.ViewModels
{
    public class PatientViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public int VisitBalance { get; set; }
        public decimal Fees { get; set; }
    }
}
