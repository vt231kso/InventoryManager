using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using System.Windows;

public partial class App : Application
{
  public static IServiceProvider? ServiceProvider { get; private set; }

  protected override void OnStartup(StartupEventArgs e)
  {
    var services = new ServiceCollection();

    services.AddDbContext<InventoryContext>(options =>
        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;"));

    ServiceProvider = services.BuildServiceProvider();

    base.OnStartup(e);
  }
}
