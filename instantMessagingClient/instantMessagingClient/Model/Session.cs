using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instantMessagingClient.Model
{
    public static class Session
    {
        public static string Token { get; set; }

        static Session()
        {
            Token = "";
        }
    }
}
