using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class InMemProductsRepository : IProductsRepository
    {
        private readonly List<Product> _products;

        public InMemProductsRepository()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Description = "Description 1",
                    Price = 1.99m,
                    CreatedAt = DateTimeOffset.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Description 2",
                    Price = 2.99m,
                    CreatedAt = DateTimeOffset.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Description = "Description 3",
                    Price = 3.99m,
                    CreatedAt = DateTimeOffset.UtcNow
                }
            };
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Task.FromResult(_products);
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_products.Find(p => p.Id == id));
        }

        public async Task CreateProductAsync(Product product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var index = _products.FindIndex(existingProduct => existingProduct.Id == product.Id);
            _products[index] = product;
            await Task.CompletedTask;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var index = _products.FindIndex(existingProduct => existingProduct.Id == id);
            _products.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}