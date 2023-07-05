using ProductCatalog.Dtos;
using ProductCatalog.Entities;

namespace ProductCatalog
{
    public static class Extensions
    {
        public static CategoryDto AsDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}