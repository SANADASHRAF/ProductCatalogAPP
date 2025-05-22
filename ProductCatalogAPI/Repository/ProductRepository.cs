using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(RepositoryContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int CategoryId, int pageNumber, int pageSize, bool trackChanges) =>
            await FindAll(trackChanges)
                .Where(p => CategoryId == 0 || p.CategoryId == CategoryId)
                .Include(p => p.Category)
                .Include(p => p.CreatedByUser)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<IEnumerable<Product>> GetProductsInCategoryAsync(int CategoryId, int pageNumber, int pageSize, bool trackChanges) =>
            await FindByCondition(p => p.CategoryId == CategoryId, trackChanges)
                .Include(p => p.Category)
                .Include(p => p.CreatedByUser)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<Product?> GetByIdAsync(long id, bool trackChanges) =>
            await FindByCondition(p => p.Id == id, trackChanges)
                .Include(p => p.Category)
                .Include(p => p.CreatedByUser)
                .FirstOrDefaultAsync();

        public void CreateProductAsync(Product product) => 
             Create(product);

        public void UpdateProductAsync(Product product)=>
            Update(product);
        

        public void DeleteProductAsync(Product product) =>
            Delete(product);

       
    }
}
