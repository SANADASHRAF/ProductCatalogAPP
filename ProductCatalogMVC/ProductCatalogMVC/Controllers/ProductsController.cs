using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogMVC.DTO;
using ProductCatalogMVC.Response;
using ProductCatalogMVC.Services;

namespace ProductCatalogMVC.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int categoryId = 0, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _productService.GetAllProductsAsync(categoryId, pageNumber, pageSize);

                return View(response);
            }
            catch
            {
                TempData["Error"] = "حدث خطأ أثناء جلب المنتجات";
                return View(new ServiceResponse<IEnumerable<ProductDto>>(false, "حدث خطأ", null));
            }
        }

    }
}
