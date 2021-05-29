using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using instantMessagingClient.P2P;
using instantMessagingCore.Models.Dto;
using Newtonsoft.Json;

namespace instantMessagingClient.Model
{
    public static class Session
    {
        public static Tokens tokens { get; set; }

        public static string sessionUsername { get; set; }

        public static SecureString sessionPassword { get; set; }

        public static bool isOnMessagingPage { get; set; }

        public static TCP communication { get; set; }

        //juste pour test
        public static string maKey { get; set; }

        public static bool hasAlreadyStarted { get; set; }
    }
}
