using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InventoryManagement.Data;
using InventoryManagement.Reports;
using InventoryManagement.Repositories;
using InventoryManagement.ViewModels;
using InventoryManagement.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace InventoryManagement
{
  public partial class MainWindow : Window
  {
    private CategoryViewModel? _categoryViewModel;
    private ProductViewModel? _productViewModel;
    private SupplierViewModel? _supplierViewModel;
    private AnalyticsViewModel? _analyticsViewModel;

    public MainWindow()
    {
      InitializeComponent();
    }

    private void CategoriesButton_Click(object sender, RoutedEventArgs e)
    {
      _categoryViewModel ??= App.ServiceProvider?.GetRequiredService<CategoryViewModel>();
      _categoryViewModel?.LoadCategories();
      ShowCategoriesView();
    }

    private void ProductsButton_Click(object sender, RoutedEventArgs e)
    {
      _productViewModel ??= App.ServiceProvider?.GetRequiredService<ProductViewModel>();
      _productViewModel?.LoadProducts();
      ShowProductsView();
    }

    private void SuppliersButton_Click(object sender, RoutedEventArgs e)
    {
      _supplierViewModel ??= App.ServiceProvider?.GetRequiredService<SupplierViewModel>();
      _supplierViewModel?.LoadSuppliers();
      ShowSuppliersView();
    }

    private void ShowCategoriesView()
    {
      if (_categoryViewModel != null)
      {
        MainContent.Content = new CategoriesView(_categoryViewModel);
        UpdateActiveButton("Категорії");
      }
    }

    private void ShowProductsView()
    {
      if (_productViewModel != null)
      {
        MainContent.Content = new ProductsView(_productViewModel);
        UpdateActiveButton("Товари");
      }
    }

    private void ShowSuppliersView()
    {
      if (_supplierViewModel != null)
      {
        MainContent.Content = new SuppliersView(_supplierViewModel);
        UpdateActiveButton("Постачальники");
      }
    }

    private void UpdateActiveButton(string activeButtonName)
    {
      foreach (var child in NavigationPanel.Children)
      {
        if (child is Button button)
        {
          button.Background = button.Content.ToString() == activeButtonName
              ? Brushes.LightBlue
              : Brushes.LightGray;
        }
      }
    }
    private void AnalyticsButton_Click(object sender, RoutedEventArgs e)
    {
      var analyticsViewModel = App.ServiceProvider?.GetRequiredService<AnalyticsViewModel>();
      var analyticsWindow = new AnalyticsView
      {
        DataContext = analyticsViewModel
      };
      analyticsWindow.Show();
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
        context.GenerateReport(
          _analyticsViewModel.Products.ToList(),
          _analyticsViewModel.CategoryDistribution.ToList(),
          _analyticsViewModel.AveragePrice,
          _analyticsViewModel.TotalQuantity,
          _analyticsViewModel.CriticalLowCount,
          saveDialog.FileName);

        MessageBox.Show("Звіт у Excel збережено успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }


    private void ExportPdfButton_Click(object sender, RoutedEventArgs e)
    {
      if (_productViewModel == null || !_productViewModel.Products.Any())
      {
        MessageBox.Show("Немає даних для експорту.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      var saveDialog = new SaveFileDialog
      {
        Filter = "PDF файл (*.pdf)|*.pdf",
        FileName = "Звіт.pdf"
      };

      if (saveDialog.ShowDialog() == true)
      {
        var strategy = new PdfReportStrategy();
        var context = new ReportContext(strategy);
        context.GenerateReport(_analyticsViewModel.Products.ToList(),
          _analyticsViewModel.CategoryDistribution.ToList(),
          _analyticsViewModel.AveragePrice,
          _analyticsViewModel.TotalQuantity,
          _analyticsViewModel.CriticalLowCount,
          saveDialog.FileName);
        MessageBox.Show("Звіт у PDF збережено успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }





  }
}
