using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class VisitTransaction :Base<int>
    {
        public int EncounterId { get; set; }
        [ForeignKey(nameof(EncounterId))]
        public Encounter Encounter { get; set; }
    }
}
