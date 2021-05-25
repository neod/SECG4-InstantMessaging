using instantMessagingServer.Models;
using instantMessagingServer.Models.Api;
using instantMessagingCore.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using instantMessagingCore.Crypto;

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

            if (ModelState.IsValid)
            {
                user.Username = user.Username.ToLower();

                DatabaseContext db = new(Configuration);
                var selectedUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == PasswordUtils.hashAndSalt(user.Password, u.Salt));
                if (selectedUser != null)
                {
                    var IDToken = Authentication.GetInstance().GetIDToken();
                    var token = JWTTokens.Generate(selectedUser.Username, IDToken, Configuration["Jwt:Key"], Configuration["Jwt:Issuer"], Int32.Parse(Configuration["Jwt:Duration"]));

                    var dbToken = db.Tokens.FirstOrDefault(t => t.UserId == selectedUser.Id);
                    var ExpirationDate = DateTime.Now.AddMinutes(Int32.Parse(Configuration["Jwt:Duration"]));
                    if (dbToken == null)
                    {
                        dbToken = new Tokens(selectedUser.Id, IDToken, ExpirationDate);
                        db.Tokens.Add(dbToken);
                    }
                    else
                    {
                        dbToken.Token = IDToken;
                        dbToken.ExpirationDate = ExpirationDate;
                        db.Tokens.Update(dbToken);
                    }

                    db.SaveChanges();

                    response = Ok(new Tokens(selectedUser.Id, token, ExpirationDate));
                }
            }
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

            if (ModelState.IsValid)
            {
                user.Username = user.Username.ToLower();

                DatabaseContext db = new(Configuration);

                if (db.Users.Any(u => u.Username == user.Username))
                {
                    response = BadRequest($"{nameof(ArgumentException)}: {nameof(user.Username)} {user.Username} is already used");
                }
                else
                {
                    var salt = PasswordUtils.getSalt();
                    var newUser = new Users(user.Username, PasswordUtils.hashAndSalt(user.Password, salt), salt);
                    db.Users.Add(newUser);

                    var IDToken = Authentication.GetInstance().GetIDToken();
                    var token = JWTTokens.Generate(user.Username, IDToken, Configuration["Jwt:Key"], Configuration["Jwt:Issuer"], Int32.Parse(Configuration["Jwt:Duration"]));
                    var ExpirationDate = DateTime.Now.AddMinutes(Int32.Parse(Configuration["Jwt:Duration"]));
                    var dbToken = new Tokens(newUser.Id, IDToken, ExpirationDate);
                    db.Tokens.Add(dbToken);

                    db.SaveChanges();

                    response = Ok(new Tokens(newUser.Id, token, ExpirationDate));
                }
            }
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
