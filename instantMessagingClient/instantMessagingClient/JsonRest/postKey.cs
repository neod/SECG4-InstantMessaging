using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instantMessagingClient.JsonRest
{
    class postKey
    {
        public int userId { get; set; }

        /// <summary>
        /// The data byte array of the key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The reference date
        /// </summary>
        public DateTime valueDate { get; set; }
    }
}
