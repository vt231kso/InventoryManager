using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repositories
{
  public class SupplierRepository : Repository<Supplier>, ISupplierRepository
  {
    public SupplierRepository(InventoryContext context) : base(context) { }

    // Синхронний метод для отримання всіх постачальників
    public IEnumerable<Supplier> GetAll()
    {
      return _context.Suppliers.ToList(); // Використовуємо ToList() для синхронного отримання всіх постачальників
    }
  }
}
