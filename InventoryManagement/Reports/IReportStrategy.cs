using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Reports
{
  public interface IReportStrategy
  {
    void GenerateReport(List<Product> products, string filePath);
  }
}
