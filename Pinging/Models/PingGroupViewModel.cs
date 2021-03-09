using System.Collections.Generic;

namespace Pinging.Models
{
    public class PingGroupViewModel
    {
        public string Address { get; set; }
        public string Dns { get; set; }
        public string Name { get; set; }
        public string Modem { get; set; }
        public List<string> Status { get; set; }
        public long RTT1 { get; set; }
        public long RTT2 { get; set; }
        public long RTT3 { get; set; }
        public long RTT4 { get; set; }
        public long RTT5 { get; set; }
    }
}