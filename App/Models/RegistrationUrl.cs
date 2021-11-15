using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class RegistrationUrl
    {
        [Key]
        public int Id { get; set; }
        public string PathUrl { get; set; }
        public string? VersionApi { get; set; }
        public char HasAdapter { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? Cooldown { get; set; }
    }
}