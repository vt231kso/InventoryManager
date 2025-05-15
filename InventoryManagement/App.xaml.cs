using Microsoft.Extensions.DependencyInjection;
using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using InventoryManagement.ViewModels;

namespace InventoryManagement
{
  public partial class App : Application
  {
    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
      try
      {
        var services = new ServiceCollection();

        // 🔌 Реєстрація контексту бази даних
        services.AddDbContext<InventoryContext>(options =>
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NewInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;"));

        // 🧱 Базовий generic репозиторій
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // 📦 Конкретні репозиторії
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        // 📦 Реєстрація ViewModel для DI
        services.AddTransient<CategoryViewModel>();
        services.AddTransient<ProductViewModel>();
        services.AddTransient<SupplierViewModel>();
        services.AddTransient<AnalyticsViewModel>();
        // 🪟 Реєстрація головного вікна
        services.AddTransient<MainWindow>();

        // Створення провайдера
        ServiceProvider = services.BuildServiceProvider();

        // 🔁 Відкриття головного вікна через DI
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
      }
      catch (Exception ex)
      {
        // Вивести повідомлення про помилку
        MessageBox.Show($"Помилка запуску: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        Shutdown(); // Закриває застосунок у разі помилки
      }

      base.OnStartup(e);
    }
  }
}
