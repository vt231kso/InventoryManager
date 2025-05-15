using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InventoryManagement.ViewModels;
using InventoryManagement.Views;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement
{
  public partial class MainWindow : Window
  {
    private CategoryViewModel? _categoryViewModel;
    private ProductViewModel? _productViewModel;
    private SupplierViewModel? _supplierViewModel;

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
  }
}
