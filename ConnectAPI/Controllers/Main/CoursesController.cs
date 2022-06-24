using Data.DAL;
using Data.Services;
using Domain.Models;
using Domain.Models.DiscussionsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IRepositoryService<Course> _repo;
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context, IRepositoryService<Course> repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet("getAllCourses/{id}")]
        public async Task<IActionResult> GetCourses(int id)
        {
            var courses = await _context.Courses.Where(c=> c.SchoolId == id).Select(c => new
            {
                c.Id,
                c.Name,
                Discussions = c.Discussions.Select(c => new { c.Id, c.Title}).ToList(),
            }).ToListAsync();

            
            return Ok(courses);
        }

        [HttpGet("getCourse/{id}")]
        public async Task<IActionResult> getCourse(int id)
        {
            var course = await _repo.GetOne(id);
            return Ok(course);
        }

          
    }
}
