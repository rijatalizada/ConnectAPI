using Authentication.Models;
using Data.DAL;
using Data.Services;
using Domain.Dtos.Discussions.Replies;
using Domain.Models.DiscussionsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepositoryService<DiscussionReply> _repo;

        public RepliesController(AppDbContext context, IRepositoryService<DiscussionReply> repo)
        {
            _context = context;
            _repo = repo;

        }


        [HttpPost("createReply")]
        public async Task<IActionResult> addReply([FromBody] ReplyPostDto replyDto)
        {
            
            var reply = new DiscussionReply
            {
                Reply = replyDto.Reply,
                UserId = replyDto.UserId,
                DiscussionId = replyDto.DiscussionId,
            };

            await _repo.Create(reply);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
