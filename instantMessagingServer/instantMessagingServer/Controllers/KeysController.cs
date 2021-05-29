using instantMessagingCore.Models.Dto;
using instantMessagingServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace instantMessagingServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KeysController : ControllerBase
    {

        private readonly IConfiguration Configuration;
        private readonly Authentication authentication;
        public KeysController(IConfiguration configuration)
        {
            Configuration = configuration;
            this.authentication = Authentication.GetInstance();
        }
        
        /// <summary>
        /// Return the selected friend user public key
        /// </summary>
        /// <param name="friendId">friend id user</param>
        /// <returns>the selecte public key</returns>
        // GET api/<KeysController>/5
        [HttpGet("get/{friendId}")]
        public IActionResult Get(int friendId)
        {
            IActionResult response = Unauthorized();

            var ClaimIDToken = User.Claims.FirstOrDefault((c) => c.Type == "IDToken");
            if (ClaimIDToken != null && authentication.isAutheticate(User.Identity.Name, ClaimIDToken))
            {
                DatabaseContext db = new(Configuration);
                var currentUser = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);

                if (db.Friends.Any(f =>
                     (f.UserId == currentUser.Id && f.FriendId == friendId) ||
                     (f.UserId == friendId && f.FriendId == currentUser.Id)
                ))
                {
                    var publicKey = db.PublicKeys.FirstOrDefault(pk => pk.UserId == friendId);
                    if (publicKey != null)
                    {
                        response = Ok(publicKey);
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Registre the user public key
        /// </summary>
        /// <param name="pk">The public key to register</param>
        /// <returns>the http status</returns>
        // POST api/<KeysController>
        [HttpPost("submit")]
        public IActionResult Post([FromBody] PublicKeys pk)
        {
            IActionResult response = Unauthorized();


            var ClaimIDToken = User.Claims.FirstOrDefault((c) => c.Type == "IDToken");
            if (ClaimIDToken != null && authentication.isAutheticate(User.Identity.Name, ClaimIDToken))
            {
                DatabaseContext db = new(Configuration);
                var currentUser = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);

                if (currentUser != null && pk.UserId == currentUser.Id)
                {

                    var dbpk = db.PublicKeys.FirstOrDefault(p => p.UserId == currentUser.Id);
                    if (dbpk == null)
                    {
                        db.PublicKeys.Add(pk);
                    }
                    else
                    {
                        dbpk.Key = pk.Key;
                        db.PublicKeys.Update(dbpk);
                    }
                    db.SaveChanges();
                    response = Ok();
                }
                else
                {
                    response = BadRequest($"{nameof(ArgumentException)}: {nameof(PublicKeys)}  does not belong to {currentUser.Username}");
                }
            }

            return response;
        }
        /*
        [Route("/{**catchAll}")]
        public IActionResult CatchAll([FromBody] object body, string catchAll)
        {
            return Ok(catchAll);
        }*/
    }
}
