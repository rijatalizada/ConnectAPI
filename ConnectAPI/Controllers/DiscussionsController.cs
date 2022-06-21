using Data.DAL;
using Data.Services;
using Domain.Models;
using Domain.Models.DiscussionsFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly IRepositoryService<Discussion> _repo;
        private readonly AppDbContext _context;

        public DiscussionsController(IRepositoryService<Discussion> repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }


        [HttpGet("getDiscussionsByCourse/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dicussion = await _context.Discussions.Where(d => d.CourseId == id).Select(d => new
            {
                d.Id,
                d.Title,
                d.CreatedDate,
                DiscussionReplies = d.Replies.Count,
                Rating = d.Ratings.Sum(s => s.GivenRating) / d.Ratings.Count,
                User = d.User.UserName
            }).ToListAsync();

            return Ok(dicussion);
        }

        [HttpGet("getTopDiscussions")]
        public async Task<IActionResult> TopDiscussions()
        {


            var discussions = await _context.Discussions.Select(d => new
            {
                d.Id,
                d.Title,
                d.CreatedDate,
                DiscussionReplies = d.Replies.Count,
                Rating = d.Ratings.Sum(s=>s.GivenRating) / d.Ratings.Count,
                User = d.User.UserName
            }).OrderByDescending(s=>s.Rating).Take(5).ToListAsync();

            return Ok(discussions);
        }


        [HttpGet("getDicsussion/{id}")]
        public async Task<IActionResult> getOne(int id)
        { 
            var discussion = await _context.Discussions.Select(d => new
            {
                d.Id,
                d.Title,
                d.Question,
                d.CreatedDate,
                d.CourseId,
                d.Course.Name,
                d.UserId,
                d.User.UserName,
                d.User.ProfileImage,
                Replies = d.Replies.Select(r => new { r.Reply, r.UserId, r.User.UserName, r.User.ProfileImage }),
                Rating = d.Ratings.Sum(s => s.GivenRating) / d.Ratings.Count,

            }).FirstOrDefaultAsync(d => d.Id == id);

            if (discussion == null) return NotFound();

            return Ok(discussion); 
        }

        

    }
}
