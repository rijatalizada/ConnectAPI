using Data.DAL;
using Data.Services;
using Domain.Dtos.Discussions;
using Domain.Models;
using Domain.Models.DiscussionsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
                CourseName = d.Course.Name,
                DiscussionReplies = d.Replies.Count,
                Rating = d.Ratings.Count > 0 ? d.Ratings.Sum(s => s.GivenRating) / d.Ratings.Count : 0,
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
                CourseName = d.Course.Name,
                Rating = d.Ratings.Count > 0 ? d.Ratings.Sum(s => s.GivenRating) / d.Ratings.Count : 0,
                User = d.User.UserName
            }).OrderByDescending(s => s.Rating).Take(5).ToListAsync();



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
                School = d.Course.School.Fullname,
                SchoolId = d.Course.School.Id,
                d.UserId,
                d.User.UserName,
                CourseName = d.Course.Name,
                d.User.ProfileImage,
                Replies = d.Replies.Select(r => new { r.Reply, r.Id, r.UserId, r.User.UserName, r.User.ProfileImage }),
                Rating = d.Ratings.Count > 0 ? d.Ratings.Sum(s => s.GivenRating) / d.Ratings.Count : 0,

            }).FirstOrDefaultAsync(d => d.Id == id);

            if (discussion == null) return NotFound();

            return Ok(discussion);
        }


        [Authorize]
        [HttpPost("createDiscussion")]
        public async Task<IActionResult> Create([FromBody] DiscussionPostDto discussionPostDto)
        {
            var discussion = new Discussion
            {
                Title = discussionPostDto.Title,
                Question = discussionPostDto.Question,
                CourseId = discussionPostDto.CourseId,
                UserId = discussionPostDto.UserId,
                CreatedDate = DateTime.Now
            };

            await _repo.Create(discussion);

            return StatusCode(StatusCodes.Status201Created);
        }



    }
}
