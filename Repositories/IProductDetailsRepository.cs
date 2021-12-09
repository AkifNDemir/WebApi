using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public interface IProductDetailsRepository
    {
        Task<IEnumerable<ProductDetails>> GetAllAsync();
        Task<IEnumerable<ProductDetails>> GetByIdAsync(Guid id);
        Task AddToIdAsync(ProductDetails productDetails);
        Task DeleteDetailsAsync(Guid id);
    }
}