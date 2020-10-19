using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using JamboPay.Helpers;
using JamboPay.Models;
using JamboPay.Repository;
using JamboPay.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JamboPay.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserNetworkRepository _userNetworkRepository;
        private readonly ICommissionRepository _commissionRepository;

        public TransactionController(ITransactionRepository transactionRepository,IServiceRepository serviceRepository, IUserNetworkRepository userNetworkRepository,ICommissionRepository commissionRepository)
        {
            _transactionRepository = transactionRepository;
            _serviceRepository = serviceRepository;
            _userNetworkRepository = userNetworkRepository;
            _commissionRepository = commissionRepository;
        }

        [Route("create")]
        public async Task<IActionResult> AddTransaction(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response {Status = "Error", Message = "fill all fields"});
            }
            
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            
            var transaction = _transactionRepository.AddTransaction(new Transaction{ServiceId = model.ServiceId,Cost = model.Cost,ApplicationUserId = userId});
            if (await _transactionRepository.SaveChangesAsync())
            {

                
                var service = await _serviceRepository.FetchService(model.ServiceId);
                var network = _userNetworkRepository.GetUserNetwork(userId);

                _commissionRepository.AddCommision(new Commission{ApplicationUserId = network==null?userId:network.Network.ApplicationUserId,Amount = service.CommissionPercentage * model.Cost, TransactionId = transaction.Id});
                if (await _commissionRepository.SaveChangesAsync())
                {
                    return Ok(new Response{Status = "success",Message = "Saved successfully"});
                }
                
            }
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("all")]
        public IEnumerable<Transaction> GetTransactions() => _transactionRepository.FetchTransactions();
    }
}