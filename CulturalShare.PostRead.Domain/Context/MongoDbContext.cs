﻿using CulturalShare.Common.Helper.Configurations;
using CulturalShare.Posts.Data.Extensions;
using MongoDB.Driver;

namespace CulturalShare.PostRead.Domain.Context;

public class MongoDbContext
{
    private IMongoDatabase _db;
    public MongoDbContext(MongoConfiguration configuration)
    {
        string _connectionString = $"{configuration.ConnectionString}/{configuration.DatabaseName}";
        MongoUrlBuilder _connection = new MongoUrlBuilder(_connectionString);
        MongoClient _client = new MongoClient(_connectionString);
        _db = _client.GetDatabase(_connection.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>()
    {
        var collectionName = typeof(T).GetTableAttributeValue();
        var name = collectionName == string.Empty ? typeof(T).Name : collectionName;
        var collection = _db.GetCollection<T>(name);
        return collection;
    }
}
