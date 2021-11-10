using System;
namespace App.Models
{
    public class list
    {
        public int id { get; set; }
        public registration_url id_registration_url { get; set; }
        public string ip_address { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string fingerprint { get; set; }
        public int router_port { get; set; }
        public int directory_port { get; set; }
        public string flag { get; set; }
        public string uptime { get; set; }
        public string version { get; set; }
        public string contactInfo { get; set; }
        public DateTime inserted { get; set; }
    }
}