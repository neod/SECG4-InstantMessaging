using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingCore.Models.Dto
{
    public class Friends
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public Friends(int userId, int friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }
    }
}
