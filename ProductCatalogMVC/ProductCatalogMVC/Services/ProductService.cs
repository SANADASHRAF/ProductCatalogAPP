using Newtonsoft.Json;
using ProductCatalogMVC.DTO;
using ProductCatalogMVC.Response;
using System.Net.Http.Headers;

namespace ProductCatalogMVC.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync(int categoryId = 0, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var response = await _httpClient.GetAsync($"products?categoryId={categoryId}&pageNumber={pageNumber}&pageSize={pageSize}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<ProductDto>>>(content);
            }
            catch
            {
                return new ServiceResponse<IEnumerable<ProductDto>>(false, "حدث خطأ أثناء جلب المنتجات", null);
            }
        }
    }
}
