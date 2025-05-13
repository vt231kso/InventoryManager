using Microsoft.Extensions.DependencyInjection;
using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace InventoryManagement
{
  public partial class App : Application
  {
    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
      var services = new ServiceCollection();

      // 🔌 Реєстрація контексту бази даних
      services.AddDbContext<InventoryContext>(options =>
          options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;"));

      // 🧱 Базовий generic репозиторій
      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

      // 📦 Конкретні репозиторії
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<ISupplierRepository, SupplierRepository>();

      // 🪟 Реєстрація головного вікна (необов’язково, але корисно для DI)
      services.AddTransient<MainWindow>();

      // Створення провайдера
      ServiceProvider = services.BuildServiceProvider();

      // 🔁 Відкриття головного вікна через DI
      var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
      mainWindow.Show();

      base.OnStartup(e);
    }
  }
}
