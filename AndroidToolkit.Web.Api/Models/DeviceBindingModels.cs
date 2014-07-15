using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AndroidToolkit.Web.Api.Models
{
    public class CreateDeviceBindingModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Year { get; set; }

        public int[] RecoveryIds { get; set; }
    }

    public class EditDeviceBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Year { get; set; }

        public string Image { get; set; }

        public int[] RecoveryIds { get; set; }
    }
}