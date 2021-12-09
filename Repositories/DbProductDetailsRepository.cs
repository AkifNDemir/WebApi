using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class DbProductDetailsRepository : IProductDetailsRepository
    {
        private const string databaseName = "webapi";
        private const string collectionName = "productDetails";
        private readonly IMongoCollection<ProductDetails> productsDetailsCollection;
        private readonly FilterDefinitionBuilder<ProductDetails> filterBuilder = Builders<ProductDetails>.Filter;
        public DbProductDetailsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            productsDetailsCollection = database.GetCollection<ProductDetails>(collectionName);
        }

        public async Task<IEnumerable<ProductDetails>> GetAllAsync()
        {
            return await productsDetailsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<IEnumerable<ProductDetails>> GetByIdAsync(Guid id)
        {
            var filter = filterBuilder.In(productDetails=>productDetails.Product , new List<Guid> { id });
            var result = await productsDetailsCollection.Find(filter).ToListAsync();
            return result;
        }

        public async Task AddToIdAsync(ProductDetails productDetails)
        {
            await productsDetailsCollection.InsertOneAsync(productDetails);
        }

        public async Task DeleteDetailsAsync(Guid id)
        {
            var filter = filterBuilder.Eq(productDetails => productDetails.Product, id);
            await productsDetailsCollection.DeleteManyAsync(filter);
        }
    }
}
    