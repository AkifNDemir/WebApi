using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class ProductDetails
    {
        public Guid Id { get; init; }
        public Guid Product { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset ChangedAt { get; init; }
    }
}