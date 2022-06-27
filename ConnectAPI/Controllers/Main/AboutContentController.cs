using Data.DAL;
using Data.Services;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectAPI.Controllers.Main
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutContentController : ControllerBase
    {
        public IRepositoryService<AboutContent> _repo;

        public AboutContentController(IRepositoryService<AboutContent> repo)
        {
            _repo = repo;
        }

        [HttpGet("GetHeader/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var header = await _repo.GetOne(id);
            return Ok(header);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut("UpdateContent/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AboutContentPostDto aboutContentPostDto)
        {
            var content = await _repo.GetOne(id);
            content.HeadingText = aboutContentPostDto.HeadingText;

            await _repo.Update(content);
            return Ok(content);
        }


    }
}