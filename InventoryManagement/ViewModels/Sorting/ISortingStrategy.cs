using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.ViewModels.Sorting
{
  public interface ISortingStrategy
  {
    IEnumerable<Product> Sort(IEnumerable<Product> products);
  }
}
