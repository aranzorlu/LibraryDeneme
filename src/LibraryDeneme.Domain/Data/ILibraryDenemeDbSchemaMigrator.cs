using System.Threading.Tasks;

namespace LibraryDeneme.Data;

public interface ILibraryDenemeDbSchemaMigrator
{
    Task MigrateAsync();
}
