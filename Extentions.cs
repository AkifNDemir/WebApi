using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi
{
    public static class Extentions
    {
        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt
            };
        }
        public static ProductDetailsDto AsDetailDto(this ProductDetails productDetails)
        {
            return new ProductDetailsDto
            {
                Id = productDetails.Id,
                Product = productDetails.Product,
                Price = productDetails.Price,
                ChangedAt = productDetails.ChangedAt
            };
        }
    }
}