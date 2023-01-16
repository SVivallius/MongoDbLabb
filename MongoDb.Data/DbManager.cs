using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb.Data;

public class DbManager
{
	private readonly IMongoClient _client;
	private readonly IMongoDatabase _database;
	private readonly IMongoCollection<BsonDocument> _collection;
	//private readonly IMongoCollection<Item> _collection;

	public DbManager(string connection, string dbName, string collectionName)
	{ 
		_client = new MongoClient(connection);
		_database = _client.GetDatabase(dbName);
        _collection = _database.GetCollection<BsonDocument>(collectionName);
        //_collection = _database.GetCollection<Item>(collectionName);
    }


	// (C)reate
	public async Task<bool> CreateOneAsync(Item item)
	{
		try
		{
			var bson = item.ToBsonDocument();
			await _collection.InsertOneAsync(bson);
			return true;
        }
		catch (Exception error)
		{
			Console.WriteLine("\tAn error occured while creating data:\n" +
				"\t" + error.Message);
			return false;
		}
	}

	// (R)ead
	public async Task<String> ReadOneAsync(string type, string name)
	{
        var filter = Builders<BsonDocument>.Filter.Eq(type, name);
		//var filter = Builders<Item>.Filter.Eq(name, type);
		var result = await _collection.FindAsync(filter);

		if (result == null) return "Could not find specified entry.";

		string fromBson = "";
		foreach (var item in result.ToEnumerable())
		{
			fromBson += "\t" + item.ToString() + "\n";
		}

		return fromBson;
	}

	// (U)pdate
	public async Task<bool> UpdateAsync(Item item, string id)
	{
        var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
        //var filter = Builders<Item>.Filter.Eq("Id", id);
        var result = await _collection.FindAsync(filter);

		if (result == null)
		{
			Console.WriteLine("\tEntry not found.\n" +
				"\tHave you double checked the ID of the item you're looking to update?");
			return false;
		}
		var bson = item.ToBsonDocument();

		var updateResult = await _collection.UpdateOneAsync(filter, bson);
		return updateResult.ModifiedCount == 1;
	}

	// (D)elete
	public async Task<bool> DeleteAsync(string id)
	{
        var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
        //var filter = Builders<Item>.Filter.Eq("Id", id);
        var checkEntry = await _collection.FindAsync(filter);
		
		if (checkEntry == null)
		{
			Console.WriteLine("\tThe entry specified could not be found.\n" +
				"Please check the ID that you provided and try again!");
			return false;
		}

		var result = _collection.DeleteOneAsync(filter);
		return result.Result.DeletedCount == 1;
	}
}

