using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using InventoryManagement.Models;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Reports
{
    public class PdfReportStrategy : IReportStrategy
    {
        private readonly Font _cyrillicFont;
        private const string DefaultFontName = "arial.ttf";
        private const int NormalFontSize = 10;
        private const int HeaderFontSize = 12;
        private const int TitleFontSize = 18;

        public PdfReportStrategy()
        {
            _cyrillicFont = InitializeCyrillicFont();
        }

        private Font InitializeCyrillicFont()
        {
            try
            {
                var fontPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Fonts), 
                    DefaultFontName);

                var baseFont = File.Exists(fontPath) 
                    ? BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                    : BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                return new Font(baseFont, NormalFontSize);
            }
            catch
            {
                return FontFactory.GetFont(FontFactory.HELVETICA, NormalFontSize);
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

            using var document = new Document(PageSize.A4, 50, 50, 25, 25);
            using var stream = new FileStream(filePath, FileMode.Create);
            using var writer = PdfWriter.GetInstance(document, stream);
            
            document.Open();
            try
            {
                AddDocumentHeader(document);
                AddSummarySection(document, averagePrice, totalQuantity, criticalLowCount);
                AddProductsTable(document, products);
                AddCategoriesTable(document, categoryDistribution);
            }
            finally
            {
                document.Close();
            }
        }

        private void AddDocumentHeader(Document document)
        {
            var titleFont = new Font(_cyrillicFont.BaseFont, TitleFontSize, Font.BOLD, BaseColor.BLUE);
            var normalFont = new Font(_cyrillicFont.BaseFont, NormalFontSize);

            document.Add(new Paragraph("Звіт по інвентаризації", titleFont));
            document.Add(new Paragraph($"Дата генерації: {DateTime.Now:dd.MM.yyyy}", normalFont));
            document.Add(Chunk.NEWLINE);
        }

        private void AddSummarySection(Document document, double averagePrice, int totalQuantity, int criticalLowCount)
        {
            var headerFont = new Font(_cyrillicFont.BaseFont, HeaderFontSize, Font.BOLD);
            var normalFont = new Font(_cyrillicFont.BaseFont, NormalFontSize);

            document.Add(new Paragraph("Загальна статистика:", headerFont));
            document.Add(new Paragraph($"Середня ціна: {averagePrice:C}", normalFont));
            document.Add(new Paragraph($"Загальна кількість: {totalQuantity}", normalFont));
            document.Add(new Paragraph($"Товарів з низьким запасом: {criticalLowCount}", normalFont));
            document.Add(Chunk.NEWLINE);
        }

        private void AddProductsTable(Document document, List<Product> products)
        {
            var table = new PdfPTable(4)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new[] { 3f, 1f, 1f, 2f });

            AddTableHeader(table, "Назва", "Ціна", "Кількість", "Категорія");
            AddProductRows(table, products);

            document.Add(new Paragraph("Список продуктів:", GetHeaderFont()));
            document.Add(table);
            document.Add(Chunk.NEWLINE);
        }

        private void AddCategoriesTable(Document document, List<CategoryCount> categories)
        {
            var table = new PdfPTable(2)
            {
                WidthPercentage = 50
            };
            table.SetWidths(new[] { 3f, 1f });

            AddTableHeader(table, "Категорія", "Кількість");
            AddCategoryRows(table, categories);

            document.Add(new Paragraph("Розподіл по категоріям:", GetHeaderFont()));
            document.Add(table);
        }

        private Font GetHeaderFont() => new(_cyrillicFont.BaseFont, HeaderFontSize, Font.BOLD);
        private Font GetNormalFont() => new(_cyrillicFont.BaseFont, NormalFontSize);

        private void AddTableHeader(PdfPTable table, params string[] headers)
        {
            foreach (var header in headers)
            {
                table.AddCell(new Phrase(header, GetHeaderFont()));
            }
        }

        private void AddProductRows(PdfPTable table, List<Product> products)
        {
            foreach (var product in products)
            {
                table.AddCell(new Phrase(product.Name, GetNormalFont()));
                table.AddCell(new Phrase(product.Price.ToString("C"), GetNormalFont()));
                table.AddCell(new Phrase(product.Quantity.ToString(), GetNormalFont()));
                table.AddCell(new Phrase(product.CategoryID.ToString(), GetNormalFont()));
            }
        }

        private void AddCategoryRows(PdfPTable table, List<CategoryCount> categories)
        {
            foreach (var category in categories)
            {
                table.AddCell(new Phrase(category.CategoryName, GetNormalFont()));
                table.AddCell(new Phrase(category.ProductCount.ToString(), GetNormalFont()));
            }
        }
    }
}