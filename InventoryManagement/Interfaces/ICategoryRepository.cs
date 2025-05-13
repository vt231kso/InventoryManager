using InventoryManagement.Models;

namespace InventoryManagement.Interfaces
{
  public interface ICategoryRepository : IRepository<Category>
  {
    //Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
  }
}
