using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingCore.Models.Dto
{
    public class Logs
    {
        /// <summary>
        /// A unique id for the entry
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The type of entry
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The entry message
        /// </summary>
        public string Message { get; set; }

        public Logs(int id, string type, string message)
        {
            Id = id;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}
