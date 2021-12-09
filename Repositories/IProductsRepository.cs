using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public interface IProductsRepository
    {
        Task CreateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
        Task UpdateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
    }

}