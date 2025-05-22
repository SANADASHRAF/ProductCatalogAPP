using AutoMapper;
using Contracts;
using Entities;
using Microsoft.AspNetCore.Identity;
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
        //private readonly UserManager<ApplicationUser> _userManager;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration, RepositoryContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userService = new Lazy<IUserService>(() => new UserService( userManager, mapper, configuration,context));
            _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper, configuration, _context));
        }
        public IProductService ProductService => _productService.Value;
        public IUserService UserService => _userService.Value;

    }
}
