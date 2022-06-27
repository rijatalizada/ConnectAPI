using AutoMapper;
using Data.DAL;
using Data.Services;
using Domain.Dtos.HomePage;
using Domain.Dtos.HomePage.Header;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderSlidersController : ControllerBase
    {

        private readonly IRepositoryService<Header> _repo;

        public HeaderSlidersController(IRepositoryService<Header> repo)
        {
            _repo = repo;
        }

        [HttpGet ("getAllHeaderItems")]
        public async Task<IActionResult> GetHeaders()
        {
            var headers = await _repo.GetAll();
            return Ok(headers);
        }


        //NO NEED FOR TITLES ONLY IMAGES

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("CreateHomeSliderItem")]
        public async Task<IActionResult> CreateHomeSliderItem([FromBody] HeaderPostDto headerPostDto)
        {
            var header = new Header()
            {
                Title = "empty",
                Image = headerPostDto.Image,
                Order = headerPostDto.Order,
            };

            await _repo.Create(header);

            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPut("UpdateHomeSliderItem/{id}")]
        public async Task<IActionResult> UpdateHomeSliderItem(int id, [FromBody] HeaderPostDto headerPostDto)
        {
            var header = await _repo.GetOne(id);
            header.Image = headerPostDto.Image;
            header.Order = headerPostDto.Order;
            await _repo.Update(header);

            return Ok();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpDelete("DeleteHomeSliderItem/{id}")]
        public async Task<IActionResult> DeleteHomeSliderItem(int id)
        {
            await _repo.Delete(id);
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
