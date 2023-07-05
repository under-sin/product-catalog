using System;

namespace ProductCatalog.Dtos
{
    public record CategoryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}