using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingServer.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime ExpirtationDate { get; set; }

        public Users(string username, string password, DateTime expirtationDate) : 
            this(-1, username, password, expirtationDate)
        {
            
        }
        

        public Users(int id, string username, string password, DateTime expirtationDate)
        {
            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            ExpirtationDate = expirtationDate;
        }
    }
}
