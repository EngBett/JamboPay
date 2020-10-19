using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JamboPay.Helpers;
using JamboPay.Models;
using JamboPay.Repository;
using JamboPay.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JamboPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly INetworkRepository _networkRepository;
        private readonly IUserNetworkRepository _userNetworkRepository;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config,
            INetworkRepository networkRepository,IUserNetworkRepository userNetworkRepository)
        {
            _userManager = userManager;
            _config = config;
            _networkRepository = networkRepository;
            _userNetworkRepository = userNetworkRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response {Status = "Error", Message = "fill all fields"});
            }

            var userExist = await _userManager.FindByEmailAsync(model.Email);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Status = "Error", Message = "User with that email exists"});
            }

            ApplicationUser user = new ApplicationUser
            {
                FullName = model.FullName,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Email = model.Email,
            };

            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                _networkRepository.AddNetwork(new Network {ApplicationUserId = user.Id});

                if (await _networkRepository.SaveChangesAsync())
                {
                    return Ok(new Response {Status = "Success", Message = "Registered successfully"});
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response {Status = "Error", Message = "Registration failed"});
        }
        
        [HttpPost("register/{networkKey}")]
        public async Task<IActionResult> Register(RegisterViewModel model,string networkKey)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response {Status = "Error", Message = "fill all fields"});
            }

            var userExist = await _userManager.FindByEmailAsync(model.Email);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Status = "Error", Message = "User with that email exists"});
            }

            ApplicationUser user = new ApplicationUser
            {
                FullName = model.FullName,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Email = model.Email,
            };

            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                

                Network network = await _networkRepository.GetNetwork(networkKey);
                    
                    _userNetworkRepository.AddUserNetwork(new UserNetwork{NetworkId = network.Id,ApplicationUserId = user.Id});

                    if (await _userNetworkRepository.SaveChangesAsync())
                    {
                        return Ok(new Response {Status = "Success", Message = "Registered successfully"});
                    }
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response {Status = "Error", Message = "Registration failed"});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response {Status = "Error", Message = "fill all fields"});
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response {Status = "Error", Message = "Authentication Failed"});
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    _config["JWT:ValidIssuer"],
                    _config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = new{fullname=user.FullName,email=user.Email}
                });
            }


            return StatusCode(StatusCodes.Status400BadRequest,
                new Response {Status = "Error", Message = "Authentication Failed"});
        }
    }
}