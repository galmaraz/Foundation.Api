using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataBase.EntityFramework.Context;

public class FoundationDbContextFactory: IDesignTimeDbContextFactory<FoundationDbContext>
{
    public  FoundationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddUserSecrets<FoundationDbContextFactory>()  
            .Build();
        
        string connectionString = configuration["ConnectionStrings:remoteConnection"];
        
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede ser nula ni estar vacía.");
        }
        
        var optionsBuilder = new DbContextOptionsBuilder<FoundationDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new FoundationDbContext(optionsBuilder.Options);
    }
}