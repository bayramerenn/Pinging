using Microsoft.AspNetCore.Mvc.Rendering;
using Pinging.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pinging.Models
{
    public class IpViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Alan zorunludur")]
        [Display(Name = "Ip Adres")]
        public string IpAddress { get; set; }

        [Required(ErrorMessage = "Alan zorunludur")]
        [Display(Name = "Ip Adı")]
        public string IpHostName { get; set; }

        [Required(ErrorMessage = "Alan zorunludur")]
        [Display(Name = "Port")]
        public string Port { get; set; }

        [Required(ErrorMessage = "Alan zorunludur")]
        [Display(Name = "Ip Adı")]
        public SelectList AdslType { get; set; }

        public int AdslTypeId { get; set; }

    }
}
