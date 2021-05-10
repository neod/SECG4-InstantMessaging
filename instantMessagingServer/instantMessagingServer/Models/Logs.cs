using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingServer.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public Logs(int id, string type, string message)
        {
            Id = id;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}
