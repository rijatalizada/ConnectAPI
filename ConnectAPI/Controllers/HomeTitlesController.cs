using Data.Services;
using Domain.Models;
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
            var titles = await _repo.GetAll();

            return Ok(titles);
        }
    }
}
