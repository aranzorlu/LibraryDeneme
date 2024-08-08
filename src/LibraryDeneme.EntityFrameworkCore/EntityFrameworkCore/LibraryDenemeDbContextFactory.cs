using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LibraryDeneme.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class LibraryDenemeDbContextFactory : IDesignTimeDbContextFactory<LibraryDenemeDbContext>
{
    public LibraryDenemeDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        LibraryDenemeEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<LibraryDenemeDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new LibraryDenemeDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LibraryDeneme.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
