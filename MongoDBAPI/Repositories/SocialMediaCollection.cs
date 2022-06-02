using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBAPI.Models;

namespace MongoDBAPI.Repositories
{
    public class SocialMediaCollection : ISocialMediaCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<SocialMedia> Collection;

        public SocialMediaCollection()
        {
            Collection = _repository.db.GetCollection<SocialMedia>("SocialMedia");
        }
        public async Task DeleteSocialMedia(string id)
        {
            var filter = Builders<SocialMedia>.Filter.Eq(s => s.id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<SocialMedia>> GetAllSocialMedia()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<SocialMedia> GetSocialMediaById(string id)
        {
            return await Collection.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task InsertSocialMedia(SocialMedia socialMedia)
        {
            await Collection.InsertOneAsync(socialMedia);
        }

        public async Task UpdateSocialMedia(SocialMedia socialMedia)
        {
            var filter = Builders<SocialMedia>.Filter.Eq(s => s.id, socialMedia.id);
            await Collection.ReplaceOneAsync(filter, socialMedia);
        }
    }
}
