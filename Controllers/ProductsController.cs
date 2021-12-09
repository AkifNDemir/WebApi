using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers{
    [Route("products")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository repository;
        private readonly IProductDetailsRepository detailsRepository;
        public ProductsController(IProductsRepository repository, IProductDetailsRepository detailsRepository)
        {
            this.repository = repository;
            this.detailsRepository = detailsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = (await repository.GetAllAsync()).Select(p => p.AsDto());
            return products;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductDto>> GetAsync(Guid id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.AsDto());
        }

        [HttpPost]

        public async Task<ActionResult<ProductDto>> PostAsync([FromBody] CreateProductDto productDto)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CreatedAt = DateTime.UtcNow,
            };
            if (product == null)
            {
                return BadRequest();
            }
            ProductDetails productDetails = new()
            {
                Id = Guid.NewGuid(),
                Product = product.Id,
                Price = product.Price,
                ChangedAt = product.CreatedAt,
            };
            await detailsRepository.AddToIdAsync(productDetails);
            await repository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetAsync), new { id = product.Id }, product.AsDto());
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateProductDto productDto)
        {
            var existingProduct = await repository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            if (productDto.Price != existingProduct.Price)
            {
                ProductDetails productDetails = new()
                {
                    Id = Guid.NewGuid(),
                    Product = id,
                    Price = productDto.Price,
                    ChangedAt = DateTime.UtcNow,
                };
                await detailsRepository.AddToIdAsync(productDetails);
            }
            Product updatedProduct = existingProduct with
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };
            await repository.UpdateProductAsync(updatedProduct);

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<ProductDto>> Delete(Guid id)
        {
            var existingProduct = await repository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            var existingProductDetails = await detailsRepository.GetByIdAsync(id);
            if (existingProductDetails != null)
            {
                await detailsRepository.DeleteDetailsAsync(id);
            }
            await repository.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
