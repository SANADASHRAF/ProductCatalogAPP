using AutoMapper;
using Contracts;
using Entities.Response;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using Service.Contracts;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly RepositoryContext _context;
        public ProductService(IRepositoryManager repository, IMapper mapper, IConfiguration configuration, RepositoryContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync(int categoryId, int pageNumber, int pageSize)
        {
            try
            {
                var products = await _repository.Product.GetAllProductsAsync(categoryId, pageNumber, pageSize, false);

                var total = await _context.Products.CountAsync(p => categoryId == 0 || p.CategoryId == categoryId);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                var pagination = new PaginationMetadata
                {
                    CurrentPage = pageNumber,
                    PerPage = pageSize,
                    Total = total,
                    LastPage = (int)Math.Ceiling((double)total / pageSize)
                };

                return new ServiceResponse<IEnumerable<ProductDto>>(true, "تم استرجاع المنتجات بنجاح", productDtos, pagination);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<ProductDto>>(false, $"خطأ أثناء استرجاع المنتجات: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(long id)
        {
            var product = await _repository.Product.GetByIdAsync(id, false);
            if (product == null)
                return new ServiceResponse<ProductDto>(false, "لم يتم العثور على المنتج", null);
            var productDto = _mapper.Map<ProductDto>(product);
            return new ServiceResponse<ProductDto>(true, "تم الاسترجاع", productDto);
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetProductsInCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {
            var products = await _repository.Product.GetProductsInCategoryAsync(categoryId, pageNumber, pageSize, false);
            var total = await _context.Products.CountAsync(p => p.CategoryId == categoryId);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            var pagination = new PaginationMetadata
            {
                CurrentPage = pageNumber,
                PerPage = pageSize,
                Total = total,
                LastPage = (int)Math.Ceiling((double)total / pageSize)
            };
            return new ServiceResponse<IEnumerable<ProductDto>>(true, "تم الاسترجاع بنجاح", productDtos, pagination);
        }

        public async Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductForCreationDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                _repository.Product.CreateProductAsync(product);
                _context.SaveChanges();
                var createdProductDto = _mapper.Map<ProductDto>(product);
                return new ServiceResponse<ProductDto>(true, "تم اضافة المنتج بنجاح", createdProductDto);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductDto>(false, $"خطأ أثناء إضافة المنتج: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse<ProductDto>> UpdateProductAsync(long id, ProductForUpdateDto productDto)
        {
            try
            {
                var product = await _repository.Product.GetByIdAsync(id, true);
                if (product == null)
                    return new ServiceResponse<ProductDto>(false, "لم يتم العثور على المنتج", null);
                _mapper.Map(productDto, product);
                _repository.Product.UpdateProductAsync(product);
                _context.SaveChanges();
                var updatedProductDto = _mapper.Map<ProductDto>(product);
                return new ServiceResponse<ProductDto>(true, "تم تحديث المنتج بنجاح", updatedProductDto);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductDto>(false, $"خطأ أثناء تحديث المنتج: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(long id)
        {
            try
            {
                var product = await _repository.Product.GetByIdAsync(id, true);
                if (product == null)
                    return new ServiceResponse<bool>(false, "لم يتم العثور على المنتج", false);
                 _repository.Product.DeleteProductAsync(product);
                _context.SaveChanges();
                return new ServiceResponse<bool>(true, "تم حذف المنتج بنجاح", true);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>(false, $"خطأ أثناء حذف المنتج: {ex.Message}", false);
            }
        }

    }
}
