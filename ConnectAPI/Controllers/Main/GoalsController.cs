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
    public class GoalsController : ControllerBase
    {
        private readonly IRepositoryService<OurGoals> _repo;

        public GoalsController(IRepositoryService<OurGoals> repo)
        {
            _repo = repo;
        }

        [HttpGet("Goals")]
        public async Task<IActionResult> GetGoals()
        {
            var goals = await _repo.GetAll();
            return Ok(goals);
        }

        [HttpGet("Goal/{id}")]
        public async Task<IActionResult> GetGoal(int id)
        {
            var goals = await _repo.GetOne(id);
            return Ok(goals);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost("CreateGoal")]
        public async Task<IActionResult> Create([FromBody] GoalPostDto goalPostDto)
        {
            var goal = new OurGoals
            {
                Goal = goalPostDto.Goal
            };

            await _repo.Create(goal);
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut("UpdateGoal/{id}")]
        public async Task<IActionResult> UpdateGoal(int id, [FromBody] GoalPostDto goalPostDto)
        {
            var goal = await _repo.GetOne(id);
            goal.Goal = goalPostDto.Goal;

            await _repo.Update(goal);
            return Ok();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("DeleteGoal/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return Ok();
        }
    }
}