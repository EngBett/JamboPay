using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        
        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> AddService(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response {Status = "Error", Message = "fill all fields"});
            }

            _serviceRepository.AddService(new Service{Name = model.Name,CommissionPercentage = model.CommissionPercentage});
            if (await _serviceRepository.SaveChangesAsync())
            {
                return Ok(new Response{Status = "success",Message = "Saved successfully"});
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("all")]
        public IEnumerable<Service> GetServices() => _serviceRepository.FetchServices();
    }
}