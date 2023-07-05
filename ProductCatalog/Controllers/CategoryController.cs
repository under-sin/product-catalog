using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Dtos;
using ProductCatalog.Entities;
using ProductCatalog.Repositories.Interfaces;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        // GET CATEGORIES /categories
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> FindAllAsync()
        {
            var categories = (await repository.FindAllAsync())
                .Select(category => category.AsDto());
            
            return categories;
        }
        
        // GET CATEGORY /categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> FindByIdAsync(Guid id)
        {
            var category = await repository.FindByIdAsync(id);

            if (category is null)
                return NotFound();

            return Ok(category.AsDto());
        }
        
        // POST CATEGORIES /categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> InsertAsync(CreateCategoryDto categoryDto)
        {
            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = categoryDto.Name
            };

            await repository.InsertAsync(category);

            return CreatedAtAction(nameof(FindByIdAsync), new { id = category.Id }, category.AsDto());
        }
        
        // PUT CATEGORIES /categories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateCategoryDto category)
        {
            var existingCategory = await repository.FindByIdAsync(id);

            if (existingCategory is null)
                return NotFound();

            // with get the reference and create a new reference with the values
            Category updateCategory = existingCategory with
            {
                Name = category.Name
            };

            await repository.UpdateAsync(updateCategory);

            return NoContent();
        }
        
        // DELETE CATEGORIES /categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAsync(Guid id)
        {
            var existingCategory = await repository.FindByIdAsync(id);

            if (existingCategory is null)
                return NotFound();

            await repository.RemoveAsync(id);

            return NoContent();
        }
        
    }
}