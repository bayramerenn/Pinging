using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pinging.Entity.Concrete
{
    public class Ip
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string IpHostName { get; set; }
        public string Port { get; set; }

        public int AdslTypeId { get; set; }

        public AdslType AdslTypes{ get; set; }
    }
}
