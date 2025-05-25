using System.Windows;
using System.Windows.Controls;
using InventoryManagement.ViewModels;
using InventoryManagement.Models;

namespace InventoryManagement.Views
{
  public partial class ProductsView : UserControl
  {
    private readonly ProductViewModel _viewModel;

    public ProductsView(ProductViewModel viewModel)
    {
      InitializeComponent();
      _viewModel = viewModel;
      DataContext = _viewModel;
    }

    //private void AddButton_Click(object sender, RoutedEventArgs e)
    //{
    //  if (!string.IsNullOrWhiteSpace(_viewModel.CurrentProduct.Name) &&
    //      _viewModel.CurrentProduct.Price > 0 &&
    //      _viewModel.CurrentProduct.Quantity >= 0)
    //  {
    //    _viewModel.AddProduct(new Product
    //    {
    //      Name = _viewModel.CurrentProduct.Name,
    //      Description = _viewModel.CurrentProduct.Description,
    //      CategoryID = _viewModel.CurrentProduct.CategoryID,
    //      SupplierID = _viewModel.CurrentProduct.SupplierID,
    //      Price = _viewModel.CurrentProduct.Price,
    //      Quantity = _viewModel.CurrentProduct.Quantity
    //    });
    //    _viewModel.CurrentProduct = new Product();
    //  }
    //}
    private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (SortComboBox.SelectedItem is ComboBoxItem selectedItem &&
          DataContext is ProductViewModel viewModel)
      {
         SortCriterion sortCriterion = selectedItem.Content.ToString() switch
         {
          "Назвою" => SortCriterion.Name,
          "Ціною" => SortCriterion.Price,
          "Кількістю" => SortCriterion.Quantity,
          _ => SortCriterion.Name
         };

         viewModel.SortProducts(sortCriterion);

      }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
      if (DataContext is ProductViewModel viewModel &&
          !string.IsNullOrWhiteSpace(viewModel.CurrentProduct.Name) &&
          viewModel.CurrentProduct.Price > 0 &&
          viewModel.CurrentProduct.Quantity >= 0)
      {
        viewModel.AddProduct(new Product
        {
          Name = viewModel.CurrentProduct.Name,
          Description = viewModel.CurrentProduct.Description,
          CategoryID = viewModel.CurrentProduct.CategoryID,
          SupplierID = viewModel.CurrentProduct.SupplierID,
          Price = viewModel.CurrentProduct.Price,
          Quantity = viewModel.CurrentProduct.Quantity
        });
      }
    }


    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
      if (_viewModel.SelectedProduct != null &&
          !string.IsNullOrWhiteSpace(_viewModel.CurrentProduct.Name))
      {
        _viewModel.SelectedProduct.Name = _viewModel.CurrentProduct.Name;
        _viewModel.SelectedProduct.Description = _viewModel.CurrentProduct.Description;
        _viewModel.SelectedProduct.CategoryID = _viewModel.CurrentProduct.CategoryID;
        _viewModel.SelectedProduct.SupplierID = _viewModel.CurrentProduct.SupplierID;
        _viewModel.SelectedProduct.Price = _viewModel.CurrentProduct.Price;
        _viewModel.SelectedProduct.Quantity = _viewModel.CurrentProduct.Quantity;

        _viewModel.UpdateProduct(_viewModel.SelectedProduct);
      }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
      if (_viewModel.SelectedProduct != null)
      {
        _viewModel.DeleteProduct(_viewModel.SelectedProduct.ProductID);
      }
    }
  }
}
