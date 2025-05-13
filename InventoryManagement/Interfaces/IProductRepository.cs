using InventoryManagement.Models;

namespace InventoryManagement.Interfaces
{
  public interface IProductRepository : IRepository<Product>
  {
    //Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    //Task<IEnumerable<Product>> GetBySupplierAsync(int supplierId);
  }
}
