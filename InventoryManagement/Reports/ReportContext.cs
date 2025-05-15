using InventoryManagement.Models;

namespace InventoryManagement.Reports
{
  public class ReportContext
  {
    private readonly IReportStrategy _strategy;

    public ReportContext(IReportStrategy strategy)
    {
      _strategy = strategy;
    }

    public void Generate(List<Product> products, string filePath)
    {
      _strategy.GenerateReport(products, filePath);
    }
  }
}
