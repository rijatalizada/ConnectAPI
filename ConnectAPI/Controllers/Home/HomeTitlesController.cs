using Data.Services;
using Domain.Dtos.HomePage.Header;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeTitlesController : ControllerBase
    {
        private readonly IRepositoryService<HomeHeaderWord> _repo;

        public HomeTitlesController(IRepositoryService<HomeHeaderWord> repo)
        {
            _repo = repo;
        }

        [HttpGet("getTitle")]
        public async Task<IActionResult> GetTitles()
        {
            var titles = await _repo.GetOne(1);

            return Ok(titles);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPut("UpdateTitle/{id}")]
        public async Task<IActionResult> UpdateTitle(int id, [FromBody] HomeHeaaderTitlesPostDto titlesDto)
        {
            var title = await _repo.GetOne(id);
            title.Title = titlesDto.Title;
            title.Subtitle = titlesDto.Subtitle;
            await _repo.Update(title);

            return Ok();
   }


    }
}
