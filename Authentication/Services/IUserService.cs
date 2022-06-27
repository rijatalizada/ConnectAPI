using Authentication.Dtos;
using Domain.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IUserService
    {
        Task<LoginResponseDto> Login(LoginModelDto model);
        Task<string> RegisterAsync(RegisterModel model);
        Task<string> UpdateAsync(UserPostDto userPostDto, User user);
    }
}
