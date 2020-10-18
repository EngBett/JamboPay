using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JamboPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JamboPay.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("all")]
        public IEnumerable<ApplicationUser> GetUsers() => _userManager.Users;

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            return Ok(new {Ambassador = await _userManager.Users.Include(t=>t.Transactions).ThenInclude(s=>s.Service).Include(c=>c.Commissions).Where(i=>i.Id==userId).FirstAsync()});
        }

        [HttpGet("balance/{userId}")]
        public async Task<IActionResult> GetCommissionBalance(string userId)
        {
            var user = await _userManager.Users.Include(c=>c.Commissions).Where(i=>i.Id==userId).FirstAsync();
            var balance = 0.0;
            foreach (var commission in user.Commissions)
            {
                balance += commission.Amount;
            }

            return Ok(new
            {
                user = new {FullName=user.FullName,Email=user.Email},
                CommissionBalance = balance
            });
        }
    }
}