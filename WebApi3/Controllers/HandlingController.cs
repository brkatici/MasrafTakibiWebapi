using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi3.Data;
using WebApi3.Models;

namespace WebApi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HandlingController : ControllerBase

    {
        private readonly AppDbContext _context;

        public HandlingController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetExpense(int id)
        {
            try
            {
                // Masrafı bul ve döndür
                Expense expense = _context.Expenses.Find(id);
                if (expense == null)
                {
                    return NotFound();
                }

                return Ok(expense);
            }
            catch (Exception ex)
            {
                // Hata durumunu uygun şekilde ele alın ve döndürün
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
