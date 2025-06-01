using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using InventoryManagement.Models;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Reports
{
    public class PdfReportStrategy : IReportStrategy
    {
        private readonly Font _normalFont;
        private readonly Font _headerFont;
        private readonly Font _titleFont;

        private const string DefaultFontName = "arial.ttf";
        private const int NormalFontSize = 10;
        private const int HeaderFontSize = 12;
        private const int TitleFontSize = 18;

        private const float PageMarginLeft = 50;
        private const float PageMarginRight = 50;
        private const float PageMarginTop = 25;
        private const float PageMarginBottom = 25;

        public PdfReportStrategy()
        {
            var baseFont = LoadBaseFont();
            _normalFont = new Font(baseFont, NormalFontSize);
            _headerFont = new Font(baseFont, HeaderFontSize, Font.BOLD);
            _titleFont = new Font(baseFont, TitleFontSize, Font.BOLD, BaseColor.BLUE);
        }

        private BaseFont LoadBaseFont()
        {
            try
            {
                var fontPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Fonts), 
                    DefaultFontName);

                return File.Exists(fontPath)
                    ? BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                    : BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Font load error: {ex.Message}");
                return BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
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
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using var stream = new FileStream(filePath, FileMode.Create);
            using var document = new Document(PageSize.A4, PageMarginLeft, PageMarginRight, PageMarginTop, PageMarginBottom);
            using var writer = PdfWriter.GetInstance(document, stream);

            document.Open();
            try
            {
                AddHeader(document);
                AddSummary(document, averagePrice, totalQuantity, criticalLowCount);
                AddProductsTable(document, products);
                AddCategoriesTable(document, categoryDistribution);
            }
            finally
            {
                document.Close();
            }
        }

        private void AddHeader(Document doc)
        {
            doc.Add(new Paragraph("Звіт по інвентаризації", _titleFont));
            doc.Add(new Paragraph($"Дата генерації: {DateTime.Now:dd.MM.yyyy}", _normalFont));
            doc.Add(Chunk.NEWLINE);
        }

        private void AddSummary(Document doc, double avgPrice, int totalQty, int lowStock)
        {
            doc.Add(new Paragraph("Загальна статистика:", _headerFont));
            doc.Add(new Paragraph($"Середня ціна: {avgPrice:C}", _normalFont));
            doc.Add(new Paragraph($"Загальна кількість: {totalQty}", _normalFont));
            doc.Add(new Paragraph($"Товарів з низьким запасом: {lowStock}", _normalFont));
            doc.Add(Chunk.NEWLINE);
        }

        private void AddProductsTable(Document doc, List<Product> products)
        {
            var table = new PdfPTable(4) { WidthPercentage = 100 };
            table.SetWidths(new[] { 3f, 1f, 1f, 2f });

            AddTableHeader(table, "Назва", "Ціна", "Кількість", "Категорія");

            foreach (var p in products)
            {
                AddTableRow(table, p.Name, p.Price.ToString("C"), p.Quantity.ToString(), p.CategoryID.ToString());
            }

            doc.Add(new Paragraph("Список продуктів:", _headerFont));
            doc.Add(table);
            doc.Add(Chunk.NEWLINE);
        }

        private void AddCategoriesTable(Document doc, List<CategoryCount> categories)
        {
            var table = new PdfPTable(2) { WidthPercentage = 50 };
            table.SetWidths(new[] { 3f, 1f });

            AddTableHeader(table, "Категорія", "Кількість");

            foreach (var c in categories)
            {
                AddTableRow(table, c.CategoryName, c.ProductCount.ToString());
            }

            doc.Add(new Paragraph("Розподіл по категоріям:", _headerFont));
            doc.Add(table);
        }

        private void AddTableHeader(PdfPTable table, params string[] headers)
        {
            foreach (var h in headers)
            {
                table.AddCell(new Phrase(h, _headerFont));
            }
        }

        private void AddTableRow(PdfPTable table, params string[] values)
        {
            foreach (var v in values)
            {
                table.AddCell(new Phrase(v, _normalFont));
            }
        }
    }
}
