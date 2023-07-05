using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Dtos
{
    public record CreateCategoryDto
    {
        [Required]
        public string Name { get; init; }
    };
}