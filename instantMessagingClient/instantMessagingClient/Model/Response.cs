using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instantMessagingClient.Model
{
    public class Response
    {
        public int userId { get; set; }

        public string token { get; set; }

        public DateTime expirationDate { get; set; }
    }
}
