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

        // GET: api/<KeysController>
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response = Unauthorized();

            DatabaseContext db = new(Configuration);
            var ClaimIDToken = User.Claims.FirstOrDefault((c) => c.Type == "IDToken");
            if (ClaimIDToken != null && authentication.isAutheticate(User.Identity.Name, ClaimIDToken))
            {
                 response = Ok(new string[] { "value1", "value2" });
            }

            return response;
        }

        // GET api/<KeysController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<KeysController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<KeysController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<KeysController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
