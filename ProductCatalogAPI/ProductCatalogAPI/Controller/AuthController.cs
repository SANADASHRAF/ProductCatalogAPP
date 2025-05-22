using Entities.Response;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared;

namespace ProductCatalogAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IServiceManager serviceManager)
        {
            _userService = serviceManager.UserService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> Register(UserForRegistrationDto registrationDto)
        {
            try
            {
                var response = await _userService.RegisterAsync(registrationDto);
                if (!response.Success)
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<UserDto>(false, $"حدث خطأ أثناء التسجيل,{ex.Message}", null));
            }
        }

   
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> Login(UserForLoginDto loginDto)
        {
            try
            {
                var response = await _userService.LoginAsync(loginDto);
                if (!response.Success)
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ServiceResponse<UserDto>(false, "حدث خطأ أثناء تسجيل الدخول", null));
            }
        }
    }
}
