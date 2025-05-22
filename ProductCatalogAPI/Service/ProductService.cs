using AutoMapper;
using Contracts;
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

    }
}
