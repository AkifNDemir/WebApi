using System;

namespace WebApi.Dtos{
    public record ProductDetailsDto
    {
        public Guid Id { get; init; }
        public Guid Product { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset ChangedAt { get; init; }

    }
}