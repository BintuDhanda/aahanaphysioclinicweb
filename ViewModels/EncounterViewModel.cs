using AahanaClinic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.ViewModels
{

    public class EncounterViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public bool IsOneTime { get; set; }
        public int? PackageId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Fees { get; set; }
        public string CheifComplaint { get; set; }
        public byte VAScale { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        // For Encouter Date Selection
        public int EncounterDateTimeDay { get; set; }
        public int EncounterDateTimeMonth { get; set; }
        public int EncounterDateTimeYear { get; set; }

        // For Encounter From Time Selection
        public int FromHour { get; set; }
        public int FromMinute { get; set; }
        public string FromMeridiem { get; set; }

        // For Encounter To Time Selection
        public int ToHour { get; set; }
        public int ToMinute { get; set; }
        public string ToMeridiem { get; set; }
        public bool BP { get; set; }
        public bool Diabetes { get; set; }
        public bool HeartStunt { get; set; }
        public bool PaceMaker { get; set; }
        public bool Allergies { get; set; }
        public bool Pregnancy { get; set; }
        public bool MetalImplant { get; set; }
        public string? Patient { get; set; }
        public int Status { get; set; }
        public int? Mode { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? Date { get; set; }
    }
}
