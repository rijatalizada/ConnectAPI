using Authentication.Dtos;
using Authentication.Services;
using Data.DAL;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;


        public AuthController(IUserService userService, AppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModelDto model)
        {
            var login = await _userService.Login(model);

            return Ok(login);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UserPostDto userPostDto )
        {
            var user = await _context.Users.FindAsync(id);
            var result = await _userService.UpdateAsync(userPostDto, user);

            return Ok(result);
        }


    }
}
