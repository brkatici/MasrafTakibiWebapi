using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi3.Data;
using WebApi3.Models;
namespace WebApi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<User> users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            User existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Username = user.Username;
            existingUser.Password = user.Password;

            _context.SaveChanges();
            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
