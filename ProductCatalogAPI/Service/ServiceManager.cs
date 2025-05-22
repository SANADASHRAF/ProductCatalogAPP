using AutoMapper;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IUserService> _userService;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration, RepositoryContext context)
        {
            _context = context;
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper, configuration, _context));
            _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper, configuration, _context));
        }
        public IProductService ProductService => _productService.Value;
        public IUserService UserService => _userService.Value;

    }
}
