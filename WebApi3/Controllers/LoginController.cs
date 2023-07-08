using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApi3.Models;
using WebApi3.Data;

namespace WebApi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly AppDbContext _context;
        public LoginController(JwtAuthenticationManager jwtAuthenticationManager,AppDbContext context)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context; 
        }


        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser(User usr)
        {
            List<User> users = _context.Users.ToList();

            var token = jwtAuthenticationManager.Authenticate(usr.Username, usr.Password,users);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [Authorize]
        [Route("TestRoute")]
        [HttpGet]
        public IActionResult test()
        {
            return Ok("Authorized");
        }
    }
    //public class UserAuth
    //{
    //    public string username { get; set; }
    //    public string password { get; set; }
    //}
}