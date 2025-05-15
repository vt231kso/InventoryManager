using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using InventoryManagement.Models;

namespace InventoryManagement.Reports
{
  public class ExcelReportStrategy : IReportStrategy
  {
    public void GenerateReport(List<Product> products, string filePath)
    {
      using var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Звіт");

      // Заголовки
      worksheet.Cell(1, 1).Value = "Назва товару";
      worksheet.Cell(1, 2).Value = "Кількість";
      worksheet.Cell(1, 3).Value = "Ціна";

      int row = 2;
      foreach (var product in products)
      {
        worksheet.Cell(row, 1).Value = product.Name;
        worksheet.Cell(row, 2).Value = product.Quantity;
        worksheet.Cell(row, 3).Value = product.Price;
        row++;
      }

      workbook.SaveAs(filePath);
    }
  }
}
