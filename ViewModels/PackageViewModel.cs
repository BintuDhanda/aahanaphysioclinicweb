﻿using AahanaClinic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.ViewModels
{
    public class PackageViewModel
    {
        public int? Id { get; set; }
        public int PatientId { get; set; }
        public string? Patient { get; set; }
        public int Mode { get; set; }
        public decimal Amount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public string? TransactionId { get; set; }
        public DateTime Date { get; set; }
        public int Visits { get; set; }
    }
}
