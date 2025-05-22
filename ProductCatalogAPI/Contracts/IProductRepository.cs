using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(int CategoryId, int pageNumber, int pageSize, bool trackChanges);
        Task<IEnumerable<Product>> GetProductsInCategoryAsync(int CategoryId, int pageNumber, int pageSize, bool trackChanges);
        Task<Product?> GetByIdAsync(long id, bool trackChanges);
        void CreateProductAsync(Product product);
        void UpdateProductAsync(Product product);
        void DeleteProductAsync(Product product);
    }
}
