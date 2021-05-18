using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingServer.Models.Dto
{
    public class PublicKeys
    {
        public int UserId { get; set; }
        public byte[] Key { get; set; }
        public DateTime ValueDate { get; set; }

        public PublicKeys(int userId, byte[] key, DateTime valueDate)
        {
            UserId = userId;
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.ValueDate = valueDate;
        }
    }
}
