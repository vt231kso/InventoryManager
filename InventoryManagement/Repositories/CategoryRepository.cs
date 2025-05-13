using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
  public class CategoryRepository : Repository<Category>, ICategoryRepository
  {
    public CategoryRepository(InventoryContext context) : base(context) { }

    // Можна додавати специфічні методи для Category тут, якщо потрібно
  }
}
