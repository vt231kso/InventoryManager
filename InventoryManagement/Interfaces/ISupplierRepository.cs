using InventoryManagement.Models;

namespace InventoryManagement.Interfaces
{
  public interface ISupplierRepository : IRepository<Supplier>
  {
    //Task<IEnumerable<Supplier>> GetSuppliersWithProductsAsync();
  }
}
