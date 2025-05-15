using InventoryManagement.Models;

namespace InventoryManagement.Interfaces
{
  public interface ICategoryRepository : IRepository<Category>
  {
    // Використовуємо синхронний метод
    IEnumerable<Category> GetAll();
  }
}
