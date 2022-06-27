using Data.DAL;
using Data.Services;
using Domain.Dtos.Courses;
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
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _context.Courses.Select(c => new
            {
                c.Id,
                c.Name,
                c.SchoolId,
                SchoolName = c.School.Fullname
            }).FirstOrDefaultAsync(c => c.Id == id);
            return Ok(course);
        }


        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CoursePostDto coursePostDto)
        {
            var course = new Course()
            {
                Name = coursePostDto.Name,
                SchoolId = coursePostDto.SchoolId,
            };

            await _repo.Create(course);
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPut("UpdateCourse/{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CoursePostDto coursePostDto)
        {
            var course = await _repo.GetOne(id);
            course.Name = coursePostDto.Name;
            course.SchoolId = coursePostDto.SchoolId;
            await _repo.Update(course);

            return Ok();
                
        }


        [Authorize(Roles = "Moderator,Admin")]
        [HttpDelete("DeleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _repo.Delete(id);
            return StatusCode(StatusCodes.Status200OK);

        }


    }
}
