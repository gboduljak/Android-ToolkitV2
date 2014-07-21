using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AndroidToolkit.Web.Api.Models
{
    public class ShowRecoveryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string Download { get; set; }

        public Object Device { get; set; }
    }

    public class CreateRecoveryModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        [Url]
        public string Download { get; set; }

        [Required]
        public int DeviceId { get; set; }
    }

    public class EditRecoveryModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        [Url]
        public string Download { get; set; }

        [Required]
        public int DeviceId { get; set; }
    }
}