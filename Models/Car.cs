using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDemo.Models
{

    [Table("Car")]
    public class Car
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int CarId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //[Required]
        [StringLength(100)]
        public string Type { get; set; }

        public byte[] ImageFile { get; set; }

        [StringLength(200)]
        public string ImageContentType { get; set; }

        [StringLength(200)]
        public string ImageFileName { get; set; }

        List<Maintenance> MaintenanceSchedule { get; set; }

        [NotMapped]
        [Required]
        public HttpPostedFileBase FileAttach { get; set; }

    }
}