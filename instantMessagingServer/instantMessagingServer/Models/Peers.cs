using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingServer.Models
{
    public class Peers
    {
        public int UserId { get; set; }
        public string Ipv4 { get; set; }
        public string Ipv6 { get; set; }
        public ushort Port { get; set; }
        public DateTime LastHeartBeat { get; set; }

        public Peers(int userId, string ipv4, string ipv6, ushort port, DateTime lastHeartBeat)
        {
            UserId = userId;
            Ipv4 = ipv4 ?? throw new ArgumentNullException(nameof(ipv4));
            Ipv6 = ipv6 ?? throw new ArgumentNullException(nameof(ipv6));
            Port = port;
            LastHeartBeat = lastHeartBeat;
        }
    }
}
