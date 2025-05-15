using System.Collections.ObjectModel;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using System.ComponentModel;

namespace InventoryManagement.ViewModels
{
  public class ProductViewModel : INotifyPropertyChanged
  {
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISupplierRepository _supplierRepository;

    public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
    public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();
    public ObservableCollection<Supplier> Suppliers { get; } = new ObservableCollection<Supplier>();

    public Product SelectedProduct { get; set; }
    public Product CurrentProduct { get; set; } = new Product();

    public ProductViewModel(
      IProductRepository productRepository,
      ICategoryRepository categoryRepository,
      ISupplierRepository supplierRepository)
    {
      _productRepository = productRepository;
      _categoryRepository = categoryRepository;
      _supplierRepository = supplierRepository;
      LoadData();
    }
    public void LoadProducts()
    {
      Products.Clear();
      var products = _productRepository.GetAll();
      foreach (var product in products)
      {
        Products.Add(product);
      }
    }

    // Синхронне завантаження даних
    public void LoadData()
    {
      var products = _productRepository.GetAll();  // Використовуємо синхронний метод GetAll
      foreach (var product in products)
        Products.Add(product);

      var categories = _categoryRepository.GetAll();
      foreach (var category in categories)
        Categories.Add(category);

      var suppliers = _supplierRepository.GetAll();
      foreach (var supplier in suppliers)
        Suppliers.Add(supplier);
    }

    public void AddProduct(Product product)
    {
      _productRepository.Add(product);
      Products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
      _productRepository.Update(product);
      var index = Products.IndexOf(Products.First(p => p.ProductID == product.ProductID));
      Products[index] = product;
    }

    public void DeleteProduct(int id)
    {
      _productRepository.Delete(_productRepository.GetById(id));
      Products.Remove(Products.First(p => p.ProductID == id));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
