using MongoDB.Driver;

namespace CommBank.Configurations;

public static class DatabaseSetup
{
    public static IServiceCollection AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
            ?? Environment.GetEnvironmentVariable("MONGO_URI")
            ?? configuration.GetConnectionString("CommBank");

        if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Contains("{CONNECTION_STRING}"))
        {
            throw new InvalidOperationException(
                "MongoDB connection string is missing or not configured. " +
                "Please specify MONGODB_URI in your .env file or server environment variables.");
        }

        services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));
        services.AddSingleton(sp => sp.GetRequiredService<IMongoClient>().GetDatabase("CommBank"));

        return services;
    }
}
