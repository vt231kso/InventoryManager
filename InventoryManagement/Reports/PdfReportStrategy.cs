using System.IO;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using InventoryManagement.Models;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Reports
{
  public class PdfReportStrategy : IReportStrategy
  {
    [DllImport("gdi32.dll")]
    private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

    private Font _cyrillicFont;

    public PdfReportStrategy()
    {
      // Ініціалізація шрифту з підтримкою кирилиці
      InitializeCyrillicFont();
    }

    private void InitializeCyrillicFont()
    {
      try
      {
        // Шлях до шрифту Arial (або іншого з підтримкою кирилиці)
        string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");

        if (File.Exists(fontPath))
        {
          var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
          _cyrillicFont = new Font(baseFont, 10);
        }
        else
        {
          // Якщо Arial не знайдено, використовуємо стандартний шрифт з попередженням
          _cyrillicFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
          MessageBox.Show("Шрифт Arial не знайдено. Деякі символи можуть відображатися неправильно.");
        }
      }
      catch
      {
        _cyrillicFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
      }
    }

    public void GenerateReport(
        List<Product> products,
        List<CategoryCount> categoryDistribution,
        double averagePrice,
        int totalQuantity,
        int criticalLowCount,
        string filePath)
    {
      // Реєструємо провайдер кодів для підтримки кирилиці
      System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

      // Створюємо документ
      Document document = new Document(PageSize.A4, 50, 50, 25, 25);

      try
      {
        // Створюємо PDF writer
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        document.Open();

        // Визначаємо шрифти
        var titleFont = new Font(_cyrillicFont.BaseFont, 18, Font.BOLD, BaseColor.BLUE);
        var headerFont = new Font(_cyrillicFont.BaseFont, 12, Font.BOLD);
        var normalFont = new Font(_cyrillicFont.BaseFont, 10);

        // Заголовок документа
        document.Add(new Paragraph("Звіт по інвентаризації", titleFont));
        document.Add(new Paragraph($"Дата генерації: {DateTime.Now.ToShortDateString()}", normalFont));
        document.Add(Chunk.NEWLINE);

        // Додаємо загальну статистику
        document.Add(new Paragraph("Загальна статистика:", headerFont));
        document.Add(new Paragraph($"Середня ціна: {averagePrice:C}", normalFont));
        document.Add(new Paragraph($"Загальна кількість: {totalQuantity}", normalFont));
        document.Add(new Paragraph($"Товарів з низьким запасом: {criticalLowCount}", normalFont));
        document.Add(Chunk.NEWLINE);

        // Додаємо таблицю продуктів
        document.Add(new Paragraph("Список продуктів:", headerFont));
        PdfPTable productsTable = new PdfPTable(4);
        productsTable.WidthPercentage = 100;
        productsTable.SetWidths(new float[] { 3, 1, 1, 2 });

        // Заголовки таблиці
        productsTable.AddCell(new Phrase("Назва", headerFont));
        productsTable.AddCell(new Phrase("Ціна", headerFont));
        productsTable.AddCell(new Phrase("Кількість", headerFont));
        productsTable.AddCell(new Phrase("Категорія", headerFont));

        // Заповнюємо таблицю даними
        foreach (var product in products)
        {
          productsTable.AddCell(new Phrase(product.Name, normalFont));
          productsTable.AddCell(new Phrase(product.Price.ToString("C"), normalFont));
          productsTable.AddCell(new Phrase(product.Quantity.ToString(), normalFont));
          productsTable.AddCell(new Phrase(product.CategoryID.ToString(), normalFont));
        }

        document.Add(productsTable);
        document.Add(Chunk.NEWLINE);

        // Додаємо розподіл по категоріям
        document.Add(new Paragraph("Розподіл по категоріям:", headerFont));
        PdfPTable categoriesTable = new PdfPTable(2);
        categoriesTable.WidthPercentage = 50;
        categoriesTable.SetWidths(new float[] { 3, 1 });

        categoriesTable.AddCell(new Phrase("Категорія", headerFont));
        categoriesTable.AddCell(new Phrase("Кількість", headerFont));

        foreach (var category in categoryDistribution)
        {
          categoriesTable.AddCell(new Phrase(category.CategoryName, normalFont));
          categoriesTable.AddCell(new Phrase(category.ProductCount.ToString(), normalFont));
        }

        document.Add(categoriesTable);
      }
      catch (DocumentException dex)
      {
        MessageBox.Show($"Помилка при створенні PDF: {dex.Message}");
        throw;
      }
      catch (IOException ioex)
      {
        MessageBox.Show($"Помилка доступу до файлу: {ioex.Message}");
        throw;
      }
      finally
      {
        document.Close();
      }
    }
  }
}
