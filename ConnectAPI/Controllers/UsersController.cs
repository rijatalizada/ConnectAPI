using Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static Data.Constants.Authorization;
namespace ConnectAPI.Controllers
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

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.Select(u => new
            {
                u.Id,
                u.Bio,
                u.FirstName,
                u.LastName,
                u.UserName,
                u.ProfileImage,
                u.isActive,
                Discussions = u.Discussions.Select(d => new
                {
                    d.Id,
                    d.CreatedDate,
                    d.Title,
                    d.Question,
                    Rating = d.Ratings.Count > 0 ? d.Ratings.Sum(s => s.GivenRating) / d.Ratings.Count : 0,
                    Replies = d.Replies.Count
                }).ToList()
            }).ToListAsync();

            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ToggleUser/{id}")]
        public async Task<IActionResult> ToggleUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return StatusCode(StatusCodes.Status404NotFound);
            user.isActive = !user.isActive;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
            
        }


        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _context.Users.Select(u => new
            {
                u.Id,
                u.UserName,
                u.FirstName,
                u.LastName,
                u.Email,
                u.isActive,
                u.ProfileImage,
                u.Bio,
                Discussions = u.Discussions.Select(d => new {
                    d.Id, 
                    d.CreatedDate, 
                    d.Title,
                    Rating = d.Ratings.Count > 0 ? d.Ratings.Sum(s => s.GivenRating) / d.Ratings.Count : 0
                }).ToList(),


            }).FirstOrDefaultAsync(u => u.Id == id);

            return Ok(user);
        }

        //[Authorize]
        //[HttpPut("UpdateUser/{id}")]
        //public async Task<IActionResult> Update(string id, [FromBody] )
        //{
            
        //}

    }
}
