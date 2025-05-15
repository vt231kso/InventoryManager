using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using InventoryManagement.Models;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Reports
{
  public class ExcelReportStrategy : IReportStrategy
  {
    public void GenerateReport(
  List<Product> products,
  List<CategoryCount> categoryDistribution,
  double averagePrice,
  int totalQuantity,
  int criticalLowCount,
  string filePath)
    {
      using var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Аналітика");

      // Виведення загальної аналітики
      worksheet.Cell(1, 1).Value = "Середня ціна:";
      worksheet.Cell(1, 2).Value = averagePrice;

      worksheet.Cell(2, 1).Value = "Загальна кількість товарів:";
      worksheet.Cell(2, 2).Value = totalQuantity;

      worksheet.Cell(3, 1).Value = "Кількість з критично низьким запасом:";
      worksheet.Cell(3, 2).Value = criticalLowCount;

      // Виведення розподілу за категоріями
      worksheet.Cell(5, 1).Value = "Категорія";
      worksheet.Cell(5, 2).Value = "Кількість товарів";

      int row = 6;
      foreach (var category in categoryDistribution)
      {
        worksheet.Cell(row, 1).Value = category.CategoryName;
        worksheet.Cell(row, 2).Value = category.ProductCount;
        row++;
      }

      // Виведення таблиці продуктів
      worksheet.Cell(row + 2, 1).Value = "Назва товару";
      worksheet.Cell(row + 2, 2).Value = "Кількість";
      worksheet.Cell(row + 2, 3).Value = "Ціна";

      int productRow = row + 3;
      foreach (var product in products)
      {
        worksheet.Cell(productRow, 1).Value = product.Name;
        worksheet.Cell(productRow, 2).Value = product.Quantity;
        worksheet.Cell(productRow, 3).Value = product.Price;
        productRow++;
      }

      workbook.SaveAs(filePath);
    }

  }
}
