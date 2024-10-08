﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AahanaClinic.Models
{
    public class Settings : Base<int>
    {
        public string Name { get; set; }
        public int Logo { get; set; }
        [ForeignKey(nameof(Logo))]
        public FileStorage LogoFile { get; set; }
        public string Address { get; set; }
        public int Signature { get; set; }
        [ForeignKey(nameof(Signature))]
        public FileStorage SignatureFile { get; set; }
        public int AccountantSignature { get; set; }
        [ForeignKey(nameof(AccountantSignature))]
        public FileStorage AccountantSignatureFile { get; set; }
    }
}
