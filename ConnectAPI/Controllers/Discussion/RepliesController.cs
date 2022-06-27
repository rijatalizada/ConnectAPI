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
        public async Task<IActionResult> AddReply([FromBody] ReplyPostDto replyDto)
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


        
        [HttpPut("UpdateReply/{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] ReplyPostDto replyPostDto )
        {
            var reply = await _repo.GetOne(id);

            if(reply.UserId == replyPostDto.UserId)
            {
                reply.Reply = replyPostDto.Reply;
                reply.UserId = replyPostDto.UserId;
                reply.DiscussionId = replyPostDto.DiscussionId;

                await _repo.Update(reply);

                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest);

        }


        [Authorize(Roles = "Reply,Moderator")]
        [HttpDelete("DeleteReply/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return StatusCode(StatusCodes.Status200OK);
        }


    }
}
