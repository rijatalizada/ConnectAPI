using AutoMapper;
using Data.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryService<School> _repo;

        public SchoolsController(IRepositoryService<School> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("getAllSchools")]
        public async Task<IActionResult> GetAll()
        {
            var schools = await _repo.GetAll();
            return Ok(schools);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var school = await _repo.GetOne(id);
            return Ok(school);
        }
    }
}
