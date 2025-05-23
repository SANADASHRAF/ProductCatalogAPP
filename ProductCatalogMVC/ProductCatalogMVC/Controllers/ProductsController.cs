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


        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int categoryId = 0, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                ServiceResponse<IEnumerable<ProductDto>> response;
                if (categoryId > 0)
                {
                    response = await _productService.GetProductsInCategoryAsync(categoryId, pageNumber, pageSize);
                }
                else
                {
                    response = await _productService.GetAllProductsAsync(categoryId, pageNumber, pageSize);
                }

                if (!response.Success)
                {
                    TempData["Error"] = response.Message;
                }
                return View(response);
            }
            catch
            {
                TempData["Error"] = "حدث خطأ أثناء جلب المنتجات";
                return View(new ServiceResponse<IEnumerable<ProductDto>>(false, "حدث خطأ", null));
            }
        }
         

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(id);
                if (!response.Success)
                {
                    TempData["Error"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                return View(response.Data);
            }
            catch
            {
                TempData["Error"] = "حدث خطأ أثناء جلب تفاصيل المنتج";
                return RedirectToAction(nameof(Index));
            }
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(id);
                if (!response.Success)
                {
                    TempData["Error"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }

                var updateDto = new ProductForUpdateDto
                {
                    Name = response.Data.Name,
                    StartDate = response.Data.StartDate,
                    Duration = response.Data.Duration.ToString(),
                    Price = response.Data.Price,
                    CategoryId = response.Data.CategoryId
                };

                return View(updateDto);
            }
            catch
            {
                TempData["Error"] = "حدث خطأ أثناء جلب المنتج";
                return RedirectToAction(nameof(Index));
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductForUpdateDto productDto)
        {
            if (!ModelState.IsValid)
                return View(productDto);

            try
            {
                var response = await _productService.UpdateProductAsync(id, productDto);
                if (!response.Success)
                {
                    TempData["Error"] = response.Message;
                    return View(productDto);
                }

                TempData["Success"] = "تم تعديل المنتج بنجاح";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل المنتج";
                return View(productDto);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(id);
                if (!response.Success)
                {
                    TempData["Error"] = response.Message;
                }
                else
                {
                    TempData["Success"] = "تم حذف المنتج بنجاح";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "حدث خطأ أثناء حذف المنتج";
                return RedirectToAction(nameof(Index));
            }
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new ProductForCreationDto());
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductForCreationDto productDto)
        {
          
            try
            {
                
                productDto.CreatedByUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
                System.Diagnostics.Debug.WriteLine("Claims in Create: " + string.Join(", ", claims));

                if (string.IsNullOrEmpty(productDto.CreatedByUserId))
                {
                    TempData["Error"] = $"معرف المستخدم غير متاح. Claims: {string.Join(", ", claims)}";
                    return View(productDto);
                }

                var response = await _productService.CreateProductAsync(productDto);
                if (!response.Success)
                {
                    TempData["Error"] = response.Message;
                    return View(productDto);
                }

                TempData["Success"] = "تم إنشاء المنتج بنجاح";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"حدث خطأ أثناء إنشاء المنتج: {ex.Message}";
                return View(productDto);
            }
        }



    }


}
