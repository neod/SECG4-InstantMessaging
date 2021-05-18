﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace instantMessagingCore.Models.Dto
{
    public class Users
    {
        /// <summary>
        /// The User id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Username
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        /// <summary>
        /// The user password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        /// <summary>
        /// The password expiration date
        /// </summary>
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
