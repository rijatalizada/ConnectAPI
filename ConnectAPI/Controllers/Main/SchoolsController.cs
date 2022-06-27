using AutoMapper;
using Data.DAL;
using Data.Services;
using Domain.Dtos;
using Domain.Models;
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
    public class SchoolsController : ControllerBase
    {

        private readonly IRepositoryService<School> _repo;
        private readonly AppDbContext _context;

        public SchoolsController(IRepositoryService<School> repo, AppDbContext context)
        {
            _repo = repo;
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

        [HttpGet("GetSchool/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok(await _repo.GetOne(id));
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("CreateSchool")]
        public async Task<IActionResult> CreateSchool([FromBody] SchoolPostDto schoolPostDto)
        {
            var school = new School()
            {
                Name = schoolPostDto.Name,
                Fullname = schoolPostDto.Fullname
            };

            await _repo.Create(school);

            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPut("UpdateSchool/{id}")]
        public async Task<IActionResult> UpdateSchool(int id ,[FromBody] SchoolPostDto schoolPost)
        {
            var school = await _repo.GetOne(id);
            school.Name = schoolPost.Name;
            school.Fullname = schoolPost.Fullname;
            await _repo.Update(school);

            return Ok();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpDelete("DeleteSchool/{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            await _repo.Delete(id);
            return StatusCode(StatusCodes.Status200OK);
        }



    }
}
