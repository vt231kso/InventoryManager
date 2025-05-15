using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Collections.Generic;
using InventoryManagement.Models; // для Product

namespace InventoryManagement.Reports
{
  public class PdfReportStrategy : IReportStrategy
  {
    public void GenerateReport(List<Product> products, string filePath)
    {
      var document = new PdfDocument();
      var page = document.AddPage();
      var gfx = XGraphics.FromPdfPage(page);
      var font = new XFont("Verdana", 12);

      int yPoint = 40;
      gfx.DrawString("Звіт по товарах", font, XBrushes.Black, new XRect(0, yPoint, page.Width, page.Height), XStringFormats.TopCenter);
      yPoint += 40;

      foreach (var product in products)
      {
        string line = $"{product.Name} - {product.Quantity} шт. - {product.Price} грн.";
        gfx.DrawString(line, font, XBrushes.Black, new XRect(40, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
        yPoint += 20;
      }

      document.Save(filePath);
    }
  }
}
