using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
  public class ProductRepository : Repository<Product>, IProductRepository
  {
    public ProductRepository(InventoryContext context) : base(context) { }

    // Можна додавати специфічні методи для Product тут, якщо потрібно
  }
}

