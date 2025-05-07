// InventoryContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using InventoryManagement.Data;

namespace InventoryManagement
{
  public class InventoryContextFactory : IDesignTimeDbContextFactory<InventoryContext>
  {
    public InventoryContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>();
      optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=InventoryDb;Trusted_Connection=True;TrustServerCertificate=True;");

      return new InventoryContext(optionsBuilder.Options);
    }
  }
}
