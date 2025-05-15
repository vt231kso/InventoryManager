using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.ViewModels
{
  public class AnalyticsViewModel : INotifyPropertyChanged
  {
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ObservableCollection<Product> Products { get; } = new();
    public ObservableCollection<CategoryCount> CategoryDistribution { get; } = new();

    public double AveragePrice => Products.Any() ? (double)Products.Average(p => p.Price) : 0.0;

    public int TotalQuantity => Products.Sum(p => p.Quantity);
    public int CriticalLowCount => Products.Count(p => p.Quantity < 5); // поріг змінний

    public AnalyticsViewModel(IProductRepository productRepo, ICategoryRepository categoryRepo)
    {
      _productRepository = productRepo;
      _categoryRepository = categoryRepo;

      LoadData();
    }

    private void LoadData()
    {
      Products.Clear();
      foreach (var product in _productRepository.GetAll())
        Products.Add(product);

      CategoryDistribution.Clear();
      var grouped = Products.GroupBy(p => p.CategoryID);
      foreach (var group in grouped)
      {
        var category = _categoryRepository.GetById(group.Key);
        CategoryDistribution.Add(new CategoryCount
        {
          CategoryName = category?.Name ?? "Невідомо",
          ProductCount = group.Count()
        });
      }

      OnPropertyChanged(nameof(AveragePrice));
      OnPropertyChanged(nameof(TotalQuantity));
      OnPropertyChanged(nameof(CriticalLowCount));
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string prop) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }

  public class CategoryCount
  {
    public string CategoryName { get; set; }
    public int ProductCount { get; set; }
  }
}
