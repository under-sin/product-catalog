using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Dtos
{
    public record UpdateCategoryDto
    {
        [Required]
        public string Name { get; init; }
    };
}