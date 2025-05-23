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
                
                var response = await _httpClient.GetAsync($"products/GetAllProducts?categoryId={categoryId}&pageNumber={pageNumber}&pageSize={pageSize}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<ProductDto>>>(content);
            }
            catch
            {
                return new ServiceResponse<IEnumerable<ProductDto>>(false, "حدث خطأ أثناء جلب المنتجات", null);
            }
        }


        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"Products/GetProductById?id={id}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<ProductDto>>(content);
            }
            catch
            {
                return new ServiceResponse<ProductDto>(false, "حدث خطأ أثناء جلب تفاصيل المنتج", null);
            }
        }


        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"Products/DeleteProduct?id={id}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<bool>>(content);
            }
            catch
            {
                return new ServiceResponse<bool>(false, "حدث خطأ أثناء حذف المنتج", false);
            }
        }


        public async Task<ServiceResponse<ProductDto>> UpdateProductAsync(int id, ProductForUpdateDto productDto)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync($"products/UpdateProduct?id={id}", productDto);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<ProductDto>>(content);
            }
            catch
            {
                return new ServiceResponse<ProductDto>(false, "حدث خطأ أثناء تعديل المنتج", null);
            }
        }


        public async Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductForCreationDto productDto)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync("products/CreateProduct", productDto);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<ProductDto>>(content);
            }
            catch
            {
                return new ServiceResponse<ProductDto>(false, "حدث خطأ أثناء إنشاء المنتج", null);
            }
        }


        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetProductsInCategoryAsync(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"products/GetProductsInCategory?categoryId={categoryId}&pageNumber={pageNumber}&pageSize={pageSize}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<ProductDto>>>(content);
            }
            catch
            {
                return new ServiceResponse<IEnumerable<ProductDto>>(false, "حدث خطأ أثناء جلب المنتجات في الفئة", null);
            }
        }

    }
}
