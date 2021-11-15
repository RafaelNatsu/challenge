using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Denylist
    {
        [Key]
        public int Id { get; set; }
        public DateTime Inserted { get; set; }

        [ForeignKey("List")]
        public int IdRef { get; set; }
    }
}