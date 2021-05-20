using instantMessagingServer.Models;
using instantMessagingServer.Models.Api;
using instantMessagingCore.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

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
            IActionResult response = Unauthorized();

            if (!ModelState.IsValid) return response;
            DatabaseContext db = new(Configuration);
            var selectedUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (selectedUser == null) return response;
            var token = JWTTokens.Generate(Configuration["Jwt:Key"], Configuration["Jwt:Issuer"]);

            var dbToken = db.Tokens.FirstOrDefault(t => t.UserId == selectedUser.Id);
            if(dbToken == null)
            {
                dbToken = new Tokens(selectedUser.Id, token, DateTime.Now.AddMinutes(JWTTokens.duration));
                db.Tokens.Add(dbToken);
            }
            else
            {
                dbToken.Token = token;
                dbToken.ExpirationDate = DateTime.Now.AddMinutes(JWTTokens.duration);
                db.Tokens.Update(dbToken);
            }
                    
            db.SaveChanges();

            response = Ok(new { token });

            return response;
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
            IActionResult response = Unauthorized();

            if (!ModelState.IsValid) return response;
            DatabaseContext db = new(Configuration);

            if (db.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest($"{nameof(ArgumentException)}: {nameof(user.Username)} {user.Username} is already used");
            }

            var newUser = new Users(user.Username, user.Password);
            db.Users.Add(newUser);

            var token = JWTTokens.Generate(Configuration["Jwt:Key"], Configuration["Jwt:Issuer"]);
            var dbToken = new Tokens(newUser.Id, token, DateTime.Now.AddMinutes(JWTTokens.duration));
            db.Tokens.Add(dbToken);

            db.SaveChanges();

            response = Ok(token);
            return response;
        }

        // DELETE api/<UsersController>/Delete/5
        /// <summary>
        /// Users Delete
        /// </summary>
        /// <param name="id">The user id to delete</param>
        [HttpDelete("Delete/{id:int}")]
        public void Delete(int id)
        {
            //TODO: implémenter la suppresion d'un utilisateur(uniquement par lui meme)
        }
    }
}
