using System;

namespace ProductCatalog.Entities
{
    public record Category
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}