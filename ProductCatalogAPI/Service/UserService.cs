using AutoMapper;
using Contracts;
using Entities.Response;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository;
using Service.Contracts;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
namespace Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly RepositoryContext _context;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration, RepositoryContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
        }

        public async Task<ServiceResponse<UserDto>> RegisterAsync(UserForRegistrationDto registrationDto)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(registrationDto);

                var result = await _userManager.CreateAsync(user, registrationDto.Password);

                var roleResult = await _userManager.AddToRoleAsync(user, "Admin");

                var registeredUserDto = _mapper.Map<UserDto>(user);
                registeredUserDto.Token = GenerateJwtToken(user);

                return new ServiceResponse<UserDto>(true, "تم التسجيل بنجاح", registeredUserDto);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UserDto>(false, $"حدث خطأ أثناء التسجيل{ex.InnerException},{ex.Message}", null);
            }
        }

        public async Task<ServiceResponse<UserDto>> LoginAsync(UserForLoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                    return new ServiceResponse<UserDto>(false, "البريد الإلكتروني أو كلمة المرور غير صحيحة", null);

                var loggedInUserDto = _mapper.Map<UserDto>(user);
                loggedInUserDto.Token = GenerateJwtToken(user);
                return new ServiceResponse<UserDto>(true, "تم تسجيل الدخول بنجاح", loggedInUserDto);
            }
            catch (Exception)
            {
                return new ServiceResponse<UserDto>(false, "حدث خطأ أثناء تسجيل الدخول", null);
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
