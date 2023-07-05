using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductCatalog.Entities;
using ProductCatalog.Repositories.Interfaces;

namespace ProductCatalog.Repositories
{
    public class MongoDbCategoryRepository : ICategoryRepository
    {

        private readonly string databaseName = "productCatalog";
        private readonly string collectionName = "catagories";
        private readonly IMongoCollection<Category> categoriesCollection;
        private readonly FilterDefinitionBuilder<Category> filterBuilder = Builders<Category>.Filter;

        public MongoDbCategoryRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            categoriesCollection = database.GetCollection<Category>(collectionName);
        }

        public async Task<IEnumerable<Category>> FindAllAsync()
        {
            return await categoriesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Category> FindByIdAsync(Guid id)
        {
            var filter = filterBuilder.Eq(category => category.Id, id);
            return await categoriesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task InsertAsync(Category category)
        {
            await categoriesCollection.InsertOneAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            var filter = filterBuilder.Eq(existingCategory => existingCategory.Id, category.Id);
            await categoriesCollection.ReplaceOneAsync(filter, category);
        }

        public async Task RemoveAsync(Guid id)
        {
            var filter = filterBuilder.Eq(category => category.Id, id);
            await categoriesCollection.DeleteOneAsync(filter);
        }
    }
}