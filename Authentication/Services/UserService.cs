using Authentication.Dtos;
using Authentication.Models;
using Data.Constants;
using Data.DAL;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly AppDbContext _context;
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _context = context;
        }

        public async Task<LoginResponseDto> Login(LoginModelDto model)
        {
            var authenticationModel = new LoginResponseDto();
            var user = await _userManager.FindByNameAsync(model.Login);
            if (user == null) user = await _userManager.FindByEmailAsync(model.Login);

            if (user == null)
            {
                authenticationModel.Message = "User Not Found";
            }


            if (!user.isActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "User is not active";
                return authenticationModel;
            }
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Login}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserId = user.Id;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();


                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Bio = "No Bio Yet",
                ProfileImage = "https://firebasestorage.googleapis.com/v0/b/connectimages-7c610.appspot.com/o/UsersImage%2FDefualtUser.png?alt=media&token=be6e1ddf-5dec-4659-8a2c-b0dac039a1e6",
                isActive = true
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
                }
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email } is already registered.";
            }
        }

        public async Task<string> UpdateAsync(UserPostDto userPostDto, User user)
        {
            var userToUpdate = user;
            if (userToUpdate == null) return $"An Error Occured, Try Later";

            if (await _userManager.CheckPasswordAsync(userToUpdate, userPostDto.ConfirmPassword)){
                if(userPostDto.Id == user.Id)
                {
                    userToUpdate.Bio = userPostDto.Bio;
                    userToUpdate.LastName = userPostDto.LastName;
                    userToUpdate.FirstName = userPostDto.FirstName;
                    userToUpdate.UserName = userPostDto.UserName;
                    userToUpdate.ProfileImage = userPostDto.ProfileImg;



                    _context.Users.Update(userToUpdate);
                    await _context.SaveChangesAsync();



                    return $"{userToUpdate.UserName} Succesfully Updated";
                }
                return "Id is not matching";
                
            } 
            if(!await _userManager.CheckPasswordAsync(userToUpdate, userPostDto.ConfirmPassword))
            {
                return "Wrong Password";
            }
            



            return "An unknown error occured, please try back later";

        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
