using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCDemo.Models
{

    [Table("Maintenance")]
    public class Maintenance
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int MaintenanceId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int Months { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}