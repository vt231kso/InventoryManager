using InventoryManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Services
{
  public class InventoryStatisticsService
  {
    public int GetTotalQuantity(IEnumerable<Product> products)
    {
      return products.Sum(p => p.Quantity);
    }

    public decimal GetTotalValue(IEnumerable<Product> products)
    {
      return products.Sum(p => p.Price * p.Quantity);
    }
  }
}
