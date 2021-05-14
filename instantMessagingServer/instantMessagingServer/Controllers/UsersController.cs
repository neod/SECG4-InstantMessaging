using instantMessagingServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace instantMessagingServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public UsersController(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        // Users inscription
        // PUT api/<UsersController>/5
        [HttpPost]
        public IActionResult Connexion([FromBody] UsersBasic user)
        {
            DatabaseContext db = new DatabaseContext(Configuration);
            if (String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password))
            {
                return BadRequest($"{nameof(ArgumentNullException)}: {nameof(user.Username)} and {nameof(user.Password)} are mendatory");
            }

            return Ok();

        }

        // Users inscription
        // PUT api/<UsersController>/5
        [HttpPut]
        public IActionResult Inscription([FromBody] UsersBasic user)
        {
            DatabaseContext db = new DatabaseContext(Configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest($"{nameof(ArgumentNullException)}: {nameof(user.Username)} and {nameof(user.Password)} are mendatory");
            }
            else if (db.Users.Where((u) => u.Username == user.Username).Count() != 0)
            {
                return BadRequest($"{nameof(ArgumentException)}: {nameof(user.Username)} {user.Username} is already used");
            }
            else
            {
                db.Users.Add(new Users(user.Username, user.Password));
                db.SaveChanges();
            }

            return Ok();

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
