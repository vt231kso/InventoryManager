using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repositories
{
  public class CategoryRepository : Repository<Category>, ICategoryRepository
  {
    public CategoryRepository(InventoryContext context) : base(context) { }

    // Синхронний метод для отримання всіх категорій
    public IEnumerable<Category> GetAll()
    {
      return _context.Categories.ToList();  // Використовуємо ToList() для синхронного отримання всіх категорій
    }
  }
}
