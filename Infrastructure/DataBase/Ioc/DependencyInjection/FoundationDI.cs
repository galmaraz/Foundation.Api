using Infrastructure.DataBase.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataBase.Ioc.DependencyInjection;

public static class FoundationDI
{
    public static IServiceCollection RegisterDataBase(this IServiceCollection collection, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:remoteConnection"];
        
        collection.AddDbContext<FoundationDbContext>(options => { options.UseNpgsql(connectionString); }
        );
        
        collection.AddDbContextFactory<FoundationDbContext>(options =>
                options.UseNpgsql(connectionString),
            ServiceLifetime.Scoped);
        return collection;
    }
    
    public static IServiceCollection RegisterLibraries(this IServiceCollection collection)
    {
        return collection;
    }
    
    public static IServiceCollection RegisterProviders(this IServiceCollection collection, IConfiguration configuration)
    {
        //collection.Configure<FirebaseOptions>(configuration.GetSection("Firebase"));
        return collection;
    }
    public static IServiceCollection RegisterServices(this IServiceCollection collection)
    {
        //Example
        //collection.AddTransient<BrandsService>();
        return collection;
    }
    
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        //Example
        //collection.AddTransient<IBrandsRepository, BrandsRepository>();
        
        return collection;
    }
}