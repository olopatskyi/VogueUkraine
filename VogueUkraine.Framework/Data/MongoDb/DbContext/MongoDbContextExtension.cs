using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VogueUkraine.Framework.Data.MongoDb.DbContext;

public static class MongoDbContextExtension
{
    public static IServiceCollection AddDbContext<TContext>(
        this IServiceCollection collection,
        Action<MongoDbContextOptions<TContext>> setupAction)
        where TContext : MongoDbContext
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

        collection.Configure(setupAction);
        return collection.AddSingleton<TContext, TContext>();
    }

    public static IServiceCollection AddDbContext<TContext>(
        this IServiceCollection collection, IConfigurationSection section)
        where TContext : MongoDbContext
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (section == null) throw new ArgumentNullException(nameof(section));

        collection.Configure<MongoDbContextOptions<TContext>>(section);
        return collection.AddSingleton<TContext, TContext>();
    }
}