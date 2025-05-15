using System.Collections.ObjectModel;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using System.ComponentModel;

namespace InventoryManagement.ViewModels
{
  public class SupplierViewModel : INotifyPropertyChanged
  {
    private readonly ISupplierRepository _repository;

    public ObservableCollection<Supplier> Suppliers { get; } = new ObservableCollection<Supplier>();
    public Supplier SelectedSupplier { get; set; }
    public Supplier CurrentSupplier { get; set; } = new Supplier();

    public SupplierViewModel(ISupplierRepository repository)
    {
      _repository = repository;
      LoadSuppliers();
    }

    // Синхронне завантаження постачальників
    public void LoadSuppliers()
    {
      var suppliers = _repository.GetAll();  // Використовуємо синхронний метод GetAll
      Suppliers.Clear();
      foreach (var supplier in suppliers)
      {
        Suppliers.Add(supplier);
      }
    }

    public void AddSupplier(Supplier supplier)
    {
      _repository.Add(supplier);
      Suppliers.Add(supplier);
    }

    public void UpdateSupplier(Supplier supplier)
    {
      _repository.Update(supplier);
      var index = Suppliers.IndexOf(Suppliers.First(s => s.SupplierID == supplier.SupplierID));
      Suppliers[index] = supplier;
    }

    public void DeleteSupplier(int id)
    {
      _repository.Delete(_repository.GetById(id));
      Suppliers.Remove(Suppliers.First(s => s.SupplierID == id));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
