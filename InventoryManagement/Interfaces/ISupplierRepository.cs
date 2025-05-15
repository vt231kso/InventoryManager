using InventoryManagement.Models;

namespace InventoryManagement.Interfaces
{
  public interface ISupplierRepository : IRepository<Supplier>
  {
    IEnumerable<Supplier> GetAll();
  }
}
