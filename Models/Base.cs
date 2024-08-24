namespace AahanaClinic.Models
{
    public class Base<TId>
    {
        public Base()
        {
            Timestamp = DateTime.Now;
        }
        public TId Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
