using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
  public class SupplierRepository : Repository<Supplier>, ISupplierRepository
  {
    public SupplierRepository(InventoryContext context) : base(context) { }

    // Можна додавати специфічні методи для Supplier тут, якщо потрібно
  }
}
