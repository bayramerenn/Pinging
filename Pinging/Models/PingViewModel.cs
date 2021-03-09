using System;

namespace Pinging.Models
{
    public class PingViewModel
    {
        public string? Adress { get; set; }
        public string? HostName { get; set; }
        public string? DnsName { get; set; }
        public string? Modem { get; set; }
        public long RoundTripTime { get; set; }
        public string? RoundTT { get; set; }
        public string? Status { get; set; }
        public DateTime? DateTime { get; set; }
        public long? TimeToLive { get; set; }
    }
}