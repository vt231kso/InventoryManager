using System.Collections.ObjectModel;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using System.ComponentModel;
using InventoryManagement.ViewModels.Sorting;
using InventoryManagement.Services;

using System.Linq;

namespace InventoryManagement.ViewModels
{
  public class ProductViewModel : INotifyPropertyChanged
  {
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISupplierRepository _supplierRepository;
    private ProductSorter _sorter = new ProductSorter(new SortByNameStrategy());
    private readonly InventoryStatisticsService _statisticsService = new InventoryStatisticsService();


    private ObservableCollection<Product> _products = new ObservableCollection<Product>();
    public ObservableCollection<Product> Products
    {
      get => _products;
      private set
      {
        _products = value;
        OnPropertyChanged(nameof(Products));
      }
    }
    public int TotalQuantity => _statisticsService.GetTotalQuantity(Products);
    public decimal TotalValue => _statisticsService.GetTotalValue(Products);
    private void RefreshStatistics()
    {
      OnPropertyChanged(nameof(TotalQuantity));
      OnPropertyChanged(nameof(TotalValue));
    }


    public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();
    public ObservableCollection<Supplier> Suppliers { get; } = new ObservableCollection<Supplier>();

    private Product _selectedProduct;
    public Product SelectedProduct
    {
      get => _selectedProduct;
      set
      {
        _selectedProduct = value;
        OnPropertyChanged(nameof(SelectedProduct));
        if (value != null)
        {
          CurrentProduct = new Product
          {
            ProductID = value.ProductID,
            Name = value.Name,
            Description = value.Description,
            CategoryID = value.CategoryID,
            SupplierID = value.SupplierID,
            Price = value.Price,
            Quantity = value.Quantity
          };
        }
      }
    }

    private Product _currentProduct = new Product();
    public Product CurrentProduct
    {
      get => _currentProduct;
      set
      {
        _currentProduct = value;
        OnPropertyChanged(nameof(CurrentProduct));
      }
    }

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
      RefreshStatistics();
    }

    public void SortProducts(string criterion)
    {
      switch (criterion)
      {
        case "Name":
          _sorter.SetStrategy(new SortByNameStrategy());
          break;
        case "Price":
          _sorter.SetStrategy(new SortByPriceStrategy());
          break;
        case "Quantity":
          _sorter.SetStrategy(new SortByQuantityStrategy());
          break;
      }

      var sortedList = _sorter.Sort(Products.ToList()).ToList();
      Products.Clear();
      foreach (var product in sortedList)
      {
        Products.Add(product);
      }
    }

    public void LoadData()
    {
      var products = _productRepository.GetAll();
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
      CurrentProduct = new Product();
      RefreshStatistics();
    }

    public void UpdateProduct(Product product)
    {
      _productRepository.Update(product);
      var index = Products.IndexOf(Products.First(p => p.ProductID == product.ProductID));
      Products[index] = product;
      RefreshStatistics();
    }

    public void DeleteProduct(int id)
    {
      _productRepository.Delete(_productRepository.GetById(id));
      Products.Remove(Products.First(p => p.ProductID == id));
      RefreshStatistics();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
