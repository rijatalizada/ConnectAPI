using Data.DAL;
using Data.Services;
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
    public class RatingsController : ControllerBase
    {
        private readonly IRepositoryService<Rating> _repo;

        public RatingsController(IRepositoryService<Rating> repo)
        {
            _repo = repo;
        }

        [HttpPost("addRating")]
        public async Task<IActionResult> AddRating([FromBody] RatingPostDto model)
        {
            var rating = new Rating
            {
                GivenRating = model.GivenRating,
                DiscussionId = model.DiscussionId,
                UserId = model.UserId,
            };

            await _repo.Create(rating);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}

