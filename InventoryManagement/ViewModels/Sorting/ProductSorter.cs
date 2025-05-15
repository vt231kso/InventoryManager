using System.Collections.Generic;
using InventoryManagement.Models;

namespace InventoryManagement.ViewModels.Sorting
{
  public class ProductSorter
  {
    private ISortingStrategy _strategy;

    public ProductSorter(ISortingStrategy strategy)
    {
      _strategy = strategy;
    }

    public void SetStrategy(ISortingStrategy strategy)
    {
      _strategy = strategy;
    }

    public IEnumerable<Product> Sort(IEnumerable<Product> products)
    {
      return _strategy.Sort(products);
    }
  }
}
