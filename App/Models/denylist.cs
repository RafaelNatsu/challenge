using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class denylist
    {
        public int id { get; set; }
        public int id_ref { get; set; }
        public DateTime inserted { get; set; }

        // [ForeignKey("id_ref")]
        // public list List { get; set; }
    }
}