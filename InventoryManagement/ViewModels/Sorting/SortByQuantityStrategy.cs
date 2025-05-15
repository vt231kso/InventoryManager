using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Models;

namespace InventoryManagement.ViewModels.Sorting
{
  public class SortByQuantityStrategy : ISortingStrategy
  {
    public IEnumerable<Product> Sort(IEnumerable<Product> products)
    {
      return products.OrderByDescending(p => p.Quantity);
    }
  }
}
