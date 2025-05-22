using System;
using Entities.Response;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Service.Contracts
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync(int categoryId, int pageNumber, int pageSize);
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(long id);
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetProductsInCategoryAsync(int categoryId, int pageNumber, int pageSize);
        Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductForCreationDto productDto);
        Task<ServiceResponse<ProductDto>> UpdateProductAsync(long id, ProductForUpdateDto productDto);
        Task<ServiceResponse<bool>> DeleteProductAsync(long id);
    }
}
