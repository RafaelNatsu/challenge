using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class ListUrl
    {
        [Key]
        public int Id { get; set; }
        
        public string IpAddress { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string? Fingerprint { get; set; }
        public int? RouterPort { get; set; }
        public int? DirectoryPort { get; set; }
        public string? Flags { get; set; }
        public string? Uptime { get; set; }
        public string? Version { get; set; }
        public string? ContactInfo { get; set; }
        public DateTime Inserted { get; set; }

        [ForeignKey("RegistrationUrl")]
        public int IdRegistrationUrl { get; set; }

    }
}