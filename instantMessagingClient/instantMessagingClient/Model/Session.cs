using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using instantMessagingCore.Models.Dto;
using Newtonsoft.Json;

namespace instantMessagingClient.Model
{
    public static class Session
    {
        public static Tokens tokens { get; set; }

        public static string sessionUsername { get; set; }

        public static SecureString sessionPassword { get; set; }
    }
}
