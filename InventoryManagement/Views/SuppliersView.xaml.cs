using System.Windows;
using System.Windows.Controls;
using InventoryManagement.ViewModels;
using InventoryManagement.Models;

namespace InventoryManagement.Views
{
  public partial class SuppliersView : UserControl
  {
    private readonly SupplierViewModel _viewModel;

    public SuppliersView(SupplierViewModel viewModel)
    {
      InitializeComponent();
      _viewModel = viewModel;
      DataContext = _viewModel;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(_viewModel.CurrentSupplier.Name) &&
          !string.IsNullOrWhiteSpace(_viewModel.CurrentSupplier.Phone))
      {
        _viewModel.AddSupplier(new Supplier
        {
          Name = _viewModel.CurrentSupplier.Name,
          Phone = _viewModel.CurrentSupplier.Phone,
          Email = _viewModel.CurrentSupplier.Email
        });
        _viewModel.CurrentSupplier = new Supplier();
      }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
      if (_viewModel.SelectedSupplier != null &&
          !string.IsNullOrWhiteSpace(_viewModel.CurrentSupplier.Name))
      {
        _viewModel.SelectedSupplier.Name = _viewModel.CurrentSupplier.Name;
        _viewModel.SelectedSupplier.Phone = _viewModel.CurrentSupplier.Phone;
        _viewModel.SelectedSupplier.Email = _viewModel.CurrentSupplier.Email;
        _viewModel.UpdateSupplier(_viewModel.SelectedSupplier);
      }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
      if (_viewModel.SelectedSupplier != null)
      {
        _viewModel.DeleteSupplier(_viewModel.SelectedSupplier.SupplierID);
      }
    }
  }
}
