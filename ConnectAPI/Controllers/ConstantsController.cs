using Data.Services;
using Domain.Models;
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
    }
}
