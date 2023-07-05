using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Entities;

namespace ProductCatalog.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> FindAllAsync();
        Task<Category> FindByIdAsync(Guid id);
        Task InsertAsync(Category category);
        Task UpdateAsync(Category category);
        Task RemoveAsync(Guid id);
    }
}