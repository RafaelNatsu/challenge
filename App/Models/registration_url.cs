using System;

namespace App.Models
{
    public class registration_url
    {
        public int id { get; set; }
        public string path_url { get; set; }
        public string? version_api { get; set; }
        public char has_adapter { get; set; }
        public DateTime date_add { get; set; }
        public DateTime? date_updated { get; set; }
        public DateTime? cooldown { get; set; }
    }
}