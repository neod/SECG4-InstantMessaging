using instantMessagingServer.Models;
using instantMessagingServer.Models.Api;
using instantMessagingCore.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // PUT api/<UsersController>/Connexion
        /// <summary>
        /// Users connexion
        /// </summary>
        /// <param name="user">The user to connect</param>
        /// <returns>The connexion token</returns>
        [HttpPost("Connexion")]
        public IActionResult Connexion([FromBody] UsersBasic user)
        {
            DatabaseContext db = new(Configuration);
            if (ModelState.IsValid)
            {
                //TODO: implémenter la connexion avec les tokens
            }

            return Ok();
        }

        // PUT api/<UsersController>/Inscription
        /// <summary>
        /// Users inscription
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("Inscription")]
        public IActionResult Inscription([FromBody] UsersBasic user)
        {
            DatabaseContext db = new(Configuration);
            if (!ModelState.IsValid)
            {
                if (db.Users.Where((u) => u.Username == user.Username).Any())
                {
                    return BadRequest($"{nameof(ArgumentException)}: {nameof(user.Username)} {user.Username} is already used");
                }
                else
                {
                    db.Users.Add(new Users(user.Username, user.Password));
                    db.SaveChanges();
                }
            }

            return Ok();

        }

        // DELETE api/<UsersController>/Delete/5
        /// <summary>
        /// Users Delete
        /// </summary>
        /// <param name="id">The user id to delete</param>
        [HttpDelete("Delete/{id}")]
        public void Delete(int id)
        {
            //TODO: implémenter la suppresion d'un utilisateur(uniquement par lui meme)
        }
    }
}
