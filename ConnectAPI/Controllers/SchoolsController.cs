using AutoMapper;
using Data.DAL;
using Data.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryService<School> _repo;
        private readonly AppDbContext _context;

        public SchoolsController(IRepositoryService<School> repo, IMapper mapper, AppDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("getAllSchools")]
        public async Task<IActionResult> GetAll()
        {
            var schools = await _context.Schools.Select(s => new
            {
                s.Id,
                s.Name,
                s.Fullname,
                Courses = s.Courses.Select(c => new { c.Id, c.Name }).ToList(),
            }).ToListAsync();

            return Ok(schools);
        }

    }
}
