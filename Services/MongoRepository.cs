using MongoDB.Bson;
using MongoDB.Driver;
using policystore.Configurations;
using System.Linq.Expressions;

namespace policystore.Services
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();

        TDocument FindById(string id);

        Task<TDocument> FindByIdAsync(string id);

        TDocument InsertOne(TDocument document);

        Task<TDocument> InsertOneAsync(TDocument document);

        void ReplaceOne(string id, TDocument document);

        Task ReplaceOneAsync(string id, TDocument document);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);
    }

    public class MongoRepository<TDocument> : IMongoRepository<TDocument>
        where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IMongoDBSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual TDocument FindById(string id)
        {
            return _collection.Find(filter => filter.Id == id).FirstOrDefault();
        }

        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                return _collection.Find(filter => filter.Id == id).FirstOrDefaultAsync();
            });
        }

        public virtual TDocument InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
            return document;
        }

        public virtual async Task<TDocument> InsertOneAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);

            return document;
        }

        public void ReplaceOne(string id,TDocument document)
        {
            _collection.FindOneAndReplace(filter => filter.Id == id, document);
        }

        public virtual async Task ReplaceOneAsync(string id, TDocument document)
        {
            await _collection.FindOneAndReplaceAsync(filter => filter.Id == id, document);
        }

        public void DeleteById(string id)
        {
            _collection.FindOneAndDelete(filter => filter.Id == id);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                _collection.FindOneAndDeleteAsync(filter => filter.Id == id);
            });
        }
    }
}
