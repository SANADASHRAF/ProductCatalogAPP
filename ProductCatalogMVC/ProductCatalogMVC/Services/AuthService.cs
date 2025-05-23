using Newtonsoft.Json;
using ProductCatalogMVC.DTO;
using ProductCatalogMVC.Response;

namespace ProductCatalogMVC.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<UserDto>> LoginAsync(UserForLoginDto loginDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Auth/Login", loginDto);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ServiceResponse<UserDto>>(content);

                return result;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UserDto>(false, $"حدث خطأ أثناء تسجيل الدخول: {ex.Message}", null);
            }
        }


        //public async Task<ServiceResponse<UserDto>> LoginAsync(UserForLoginDto loginDto)
        //{
        //    try
        //    {
        //        var response = await _httpClient.PostAsJsonAsync("Auth/Login", loginDto);
        //        var content = await response.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<ServiceResponse<UserDto>>(content);

        //        if (result.Success && result.Data != null)
        //        {
        //            System.Diagnostics.Debug.WriteLine("Token: " + result.Data.Token);
        //            _httpContextAccessor.HttpContext.Session.SetString("JwtToken", result.Data.Token);
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ServiceResponse<UserDto>(false, $"حدث خطأ أثناء تسجيل الدخول{ex.Message}", null);
        //    }
        //}


    }
}
