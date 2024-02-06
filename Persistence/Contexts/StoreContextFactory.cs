using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.Contexts;

public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
{
    public StoreContext CreateDbContext(string[] args) =>
        new (new DbContextOptionsBuilder<StoreContext>().UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=StoreDb").Options);
}
