using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RconnectAPI.Models;

namespace RconnectAPI.Services;

public class HobbyService
{
    private readonly IMongoCollection<Hobby> _hobbyCollection;

    public HobbyService(IOptions<MongoDbSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionUri);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _hobbyCollection = database.GetCollection<Hobby>("hobbies");
    }

    public async Task<List<Hobby>> GetAsync() =>
        await _hobbyCollection.Find(_ => true).ToListAsync();

    public async Task<Hobby?> GetAsync(string id) =>
        await _hobbyCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Hobby newUser) =>
        await _hobbyCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, Hobby updatedBook) =>
        await _hobbyCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _hobbyCollection.DeleteOneAsync(x => x.Id == id);
}