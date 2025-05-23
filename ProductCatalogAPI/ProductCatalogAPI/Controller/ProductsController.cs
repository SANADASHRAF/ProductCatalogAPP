using AutoMapper;
using Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared;

namespace ProductCatalogAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IServiceManager serviceManager)
        {
            _productService = serviceManager.ProductService;
        }

        [HttpGet ("GetAllProducts")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDto>>>> GetAllProducts
            (int categoryId = 0, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _productService.GetAllProductsAsync(categoryId, pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ServiceResponse<IEnumerable<ProductDto>>(false, "حدث خطأ أثناء جلب المنتجات", null));
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetProductById")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> GetProductById(long id)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(id);

                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ServiceResponse<ProductDto>(false, "حدث خطأ أثناء جلب المنتج", null));
            }
        }



        [HttpGet("GetProductsInCategory")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDto>>>> GetProductsInCategory
            (int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _productService.GetProductsInCategoryAsync(categoryId, pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ServiceResponse<IEnumerable<ProductDto>>(false, "حدث خطأ أثناء جلب المنتجات في الفئة", null));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost ("CreateProduct")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> CreateProduct
            (ProductForCreationDto productDto)
        {
            try
            {  
                var response = await _productService.CreateProductAsync(productDto);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ServiceResponse<ProductDto>(false, "حدث خطأ أثناء إنشاء المنتج", null));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> UpdateProduct
            (long id, ProductForUpdateDto productDto)
        {
            try
            {
                var response = await _productService.UpdateProductAsync(id, productDto);
                if (!response.Success)
                    return NotFound(response);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ServiceResponse<ProductDto>(false, "حدث خطأ أثناء تعديل المنتج", null));
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct(long id)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(id);

                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ServiceResponse<bool>(false, "حدث خطأ أثناء حذف المنتج", false));
            }
        }

    }
}
