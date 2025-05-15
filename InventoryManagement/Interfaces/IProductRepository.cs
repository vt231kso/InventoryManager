using InventoryManagement.Models;

namespace InventoryManagement.Interfaces
{
  public interface IProductRepository : IRepository<Product>
  {
    IEnumerable<Product> GetAll();
  }
}
