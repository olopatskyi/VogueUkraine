using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace VogueUkraine.Framework.Data.MongoDb.DbContext;

/// <summary>
/// Abstraction over mongodb driver.
/// </summary>
public abstract class MongoDbContext : IMongoDbContext
{
    public IMongoDatabase Database { get; }

    protected MongoDbContext(MongoDbContextOptions options)
    {
        var client = new MongoClient(options.ConnectionString);
        Database = client.GetDatabase(options.Database);
        InitializeCollections();
    }

    private void InitializeCollections()
    {
        MethodInfo getCollectionMethod = typeof(IMongoDatabase).GetMethod("GetCollection");
        foreach (var propertyInfo in GetType().GetProperties()
                     .Where(p => p.PropertyType.IsGenericType &&
                                 p.PropertyType.GetGenericTypeDefinition() == typeof(IMongoCollection<>)))
        {
            if (getCollectionMethod == null)
            {
                throw new NullReferenceException("GetCollection method not found");
            }

            if (!propertyInfo.CanWrite) continue;

            var genericArgument = propertyInfo.PropertyType.GetGenericArguments()[0];
            var tableAttribute = genericArgument.GetCustomAttribute<TableAttribute>();
            var collectionName = tableAttribute != null ? tableAttribute.Name : genericArgument.Name.ToLower() + "s";

            var genericMethod = getCollectionMethod.MakeGenericMethod(genericArgument);
            var collection = genericMethod.Invoke(Database, new object[] { collectionName, null });
            propertyInfo.SetValue(this, collection);
            Console.WriteLine($"{propertyInfo.Name} => {collectionName}");
        }
    }

    public virtual Task MigrateAsync()
    {
        Console.WriteLine("Applying migration...");

        Console.WriteLine("Migration applied successfully");
        return Task.CompletedTask;
    }
}