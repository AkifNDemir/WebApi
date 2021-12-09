using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public record CreateProductDto
    {
        [Required]
        public string Name { get; init; }
        public string Description { get; init; }
        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; init; }
        
    }
}