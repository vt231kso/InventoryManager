using InventoryManagement.Models;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Reports
{
  public class ReportContext
  {
    private readonly IReportStrategy _strategy;

    public ReportContext(IReportStrategy strategy)
    {
      _strategy = strategy;
    }

    //public void Generate(List<Product> products, string filePath)
    //{
    //  _strategy.GenerateReport(products, filePath);
    //}
    public void GenerateReport(
  List<Product> products,
  List<CategoryCount> categoryDistribution,
  double averagePrice,
  int totalQuantity,
  int criticalLowCount,
  string filePath)
    {
      _strategy.GenerateReport(products, categoryDistribution, averagePrice, totalQuantity, criticalLowCount, filePath);
    }

  }
}
