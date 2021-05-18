using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingCore.Models.Dto
{
    public class Tokens
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Tokens(int userId, string token, DateTime expirationDate)
        {
            UserId = userId;
            Token = token ?? throw new ArgumentNullException(nameof(token));
            this.ExpirationDate = expirationDate;
        }
    }
}
