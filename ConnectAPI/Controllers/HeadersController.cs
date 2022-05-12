using AutoMapper;
using Data.DAL;
using Data.Services;
using Domain.Dtos.HomePage;
using Domain.Dtos.HomePage.Header;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryService<Header> _repo;

        public HeadersController(IRepositoryService<Header> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet ("getAllHeaderItems")]
        public async Task<IActionResult> GetHeaders()
        {
            var headers = await _repo.GetAll();
            return Ok(headers);
        }
    }
}
