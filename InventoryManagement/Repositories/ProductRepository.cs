using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repositories
{
  public class ProductRepository : Repository<Product>, IProductRepository
  {
    public ProductRepository(InventoryContext context) : base(context) { }

    // Синхронний метод для отримання всіх продуктів
    public IEnumerable<Product> GetAll()
    {
      return _context.Products.ToList(); // Використовуємо ToList() для синхронного отримання всіх продуктів
    }
  }
}
