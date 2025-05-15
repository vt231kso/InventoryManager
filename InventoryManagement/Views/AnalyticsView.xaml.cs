using Microsoft.Win32;
using System.Linq;
using System.Windows;
using InventoryManagement.ViewModels;
using InventoryManagement.Reports; // Простір імен, де твоя стратегія

namespace InventoryManagement.Views
{

  public partial class AnalyticsView : Window
  {
    private AnalyticsViewModel _analyticsViewModel => DataContext as AnalyticsViewModel;

    public AnalyticsView()
    {
      InitializeComponent();
    }

    private void ExportExcelButton_Click(object sender, RoutedEventArgs e)
    {
      if (_analyticsViewModel == null || !_analyticsViewModel.Products.Any())
      {
        MessageBox.Show("Немає даних для експорту.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      var saveDialog = new SaveFileDialog
      {
        Filter = "Excel файл (*.xlsx)|*.xlsx",
        FileName = "Аналітика.xlsx"
      };

      if (saveDialog.ShowDialog() == true)
      {
        var strategy = new ExcelReportStrategy();
        var context = new ReportContext(strategy);
        context.Generate(_analyticsViewModel.Products.ToList(), saveDialog.FileName);
        MessageBox.Show("Звіт у Excel збережено успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }

    private void ExportPdfButton_Click(object sender, RoutedEventArgs e)
    {
      if (_analyticsViewModel == null || !_analyticsViewModel.Products.Any())
      {
        MessageBox.Show("Немає даних для експорту.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      var saveDialog = new SaveFileDialog
      {
        Filter = "PDF файл (*.pdf)|*.pdf",
        FileName = "Аналітика.pdf"
      };

      if (saveDialog.ShowDialog() == true)
      {
        var strategy = new PdfReportStrategy();
        var context = new ReportContext(strategy);
        context.Generate(_analyticsViewModel.Products.ToList(), saveDialog.FileName);
        MessageBox.Show("Звіт у PDF збережено успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }
  }
}
