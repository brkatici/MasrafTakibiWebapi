using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi3;
using WebApi3.Data;
using WebApi3.Models;

namespace ExpenseManager.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        
        public IActionResult GetAllExpenses()
        {
            List<Expense> expenses = _context.Expenses.ToList();
            return Ok(expenses);
        }

        //Toplam Harcama Endpointi Kişi Bazlı..
        [HttpGet("{id}")]
        public IActionResult GetTotalExpenses(string id)
        {
            //string authenticatedUserId = GetAuthenticatedUserId();
            decimal? totalExpenses = _context.Expenses
                .Where(e => e.UserId == id)
                .Sum(e => e.Amount);
            return Ok(totalExpenses);
        }

        [HttpPost]
        public IActionResult CreateExpense(Expense expense)
        {
            expense.CreatedDate = DateTime.Now;
            expense.UserId = expense.UserId;
            _context.Expenses.Add(expense);
            _context.SaveChanges();
            return Ok(expense);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, Expense expense)
        {
            Expense existingExpense = _context.Expenses.Find(id);
            if (existingExpense == null)
            {
                return NotFound();
            }

            existingExpense.Title = expense.Title;
            existingExpense.Amount = expense.Amount;
            existingExpense.Category = expense.Category;
            _context.SaveChanges();
            return Ok(existingExpense);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            Expense expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("aggregate")]
        public IActionResult AggregateDailyExpenses()
        {
            // Kullanıcı bazlı masrafları günlük olarak aggregate etmek için işlemler yapılır
            return Ok();
        }

        private string GetAuthenticatedUserId()
        {
            
            return User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        }
    }
}
