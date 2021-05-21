using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace instantMessagingServer.Models
{
    public class Authentication
    {
        private static Authentication authentication;

        public static Authentication GetInstance() => authentication ??= new Authentication();

        private Authentication()
        {
        }

        public bool isAutheticate(string name, Claim ClaimIDToken)
        {
            DatabaseContext db = new(Config.Configuration);
            var user = db.Users.FirstOrDefault(u => u.Username == name);
            return db.Tokens.Any(t => t.Token == ClaimIDToken.Value && t.UserId == user.Id);
        }
    }
}
