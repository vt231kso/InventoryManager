using System.Windows;
using System.Windows.Controls;
using InventoryManagement.ViewModels;
using InventoryManagement.Models;

namespace InventoryManagement.Views
{
  public partial class CategoriesView : UserControl
  {
    private readonly CategoryViewModel _viewModel;

    public CategoriesView(CategoryViewModel viewModel)
    {
      InitializeComponent();
      _viewModel = viewModel;
      DataContext = _viewModel;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(_viewModel.CurrentCategory.Name))
      {
        _viewModel.AddCategory(new Category
        {
          Name = _viewModel.CurrentCategory.Name
        });
        _viewModel.CurrentCategory = new Category();
      }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
      if (_viewModel.SelectedCategory != null &&
          !string.IsNullOrWhiteSpace(_viewModel.CurrentCategory.Name))
      {
        _viewModel.SelectedCategory.Name = _viewModel.CurrentCategory.Name;
        _viewModel.UpdateCategory(_viewModel.SelectedCategory);
      }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
      if (_viewModel.SelectedCategory != null)
      {
        _viewModel.DeleteCategory(_viewModel.SelectedCategory.CategoryID);
      }
    }
  }
}
