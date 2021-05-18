using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingCore.Models.Dto
{
    public class Friends
    {
        /// <summary>
        /// The owner id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The owner friend
        /// </summary>
        public int FriendId { get; set; }

        public Friends(int userId, int friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }
    }
}
