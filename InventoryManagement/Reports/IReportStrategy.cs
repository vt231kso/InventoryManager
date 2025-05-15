using InventoryManagement.Models;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Reports
{
  public interface IReportStrategy
  {
    void GenerateReport(
      List<Product> products,
      List<CategoryCount> categoryDistribution,
      double averagePrice,
      int totalQuantity,
      int criticalLowCount,
      string filePath);
  }

}
