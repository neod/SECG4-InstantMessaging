﻿using instantMessagingServer.Models.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingServer.Models.Dto
{
    public class Users : UsersBasic
    {
        public int Id { get; set; }
        public DateTime ExpirtationDate { get; set; }

        public Users()
        {
            ExpirtationDate = DateTime.Now;
        }

        public Users(string username, string password)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            ExpirtationDate = DateTime.Now;
        }
        
        public Users(int id, string username, string password)
        {
            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            ExpirtationDate = DateTime.Now;
        }
    }
}