using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApi3.Models;
using WebApi3.Data;

namespace WebApi3
{
    public class JwtAuthenticationManager
    {
        //key declaration
        private readonly string key;
        private readonly AppDbContext _context;

        public JwtAuthenticationManager(string key, AppDbContext context)
        {
            this.key = key;

            this._context = context;    
        }
       
        public string Authenticate(string username, string password , List<User> users)
        {
            //auth failed - creds incorrect
            if (!users.Any(u => u.Username == username && u.Password == password))
            {
                return null;
            }
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                //set duration of token here
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature) //setting sha256 algorithm
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}