using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUserRepository> _user;
        private readonly Lazy<IProductRepository> _product;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _user = new Lazy<IUserRepository>(() => new UserRepository(_repositoryContext));
            _product = new Lazy<IProductRepository>(() => new ProductRepository(_repositoryContext));
        }

        public IUserRepository User => _user.Value;
        public IProductRepository Product => _product.Value;

        public void Save() => _repositoryContext.SaveChanges();
    }
}
