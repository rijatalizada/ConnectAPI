using Data.Services;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstantsController : ControllerBase
    {
        private readonly IRepositoryService<Constant> _repo;

        public ConstantsController(IRepositoryService<Constant> repo)
        {
            _repo = repo;
        }

        [HttpGet("getConsntans")]
        public async Task<IActionResult> getConstants()
        {
            var constants = await _repo.GetOne(1);
            return Ok(constants);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPut("UpdateConstants/{id}")]
        public async Task<IActionResult> UpdateConstants([FromRoute] int id ,[FromBody] ConstantsPostDto constantsPostDto )
        {
            var constant = await _repo.GetOne(id);
            if (constant == null) return StatusCode(StatusCodes.Status404NotFound);
            constant.LogoURL = constantsPostDto.LogoURL;
            constant.TwitterUrl = constantsPostDto.TwitterUrl;
            constant.InstagramUrl = constantsPostDto.InstagramUrl;
            constant.RedditUrl = constantsPostDto.RedditUrl;
            constant.FacebookUrl = constantsPostDto.FacebookUrl;

            await _repo.Update(constant);

            return Ok();
        }
    }
}
