using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Models;

namespace InventoryManagement.ViewModels.Sorting
{
  public class SortByPriceStrategy : ISortingStrategy
  {
    public IEnumerable<Product> Sort(IEnumerable<Product> products)
    {
      return products.OrderBy(p => p.Price);
    }
  }
}
