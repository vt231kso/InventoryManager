using System.Collections.ObjectModel;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using System.ComponentModel;

namespace InventoryManagement.ViewModels
{
  public class CategoryViewModel : INotifyPropertyChanged
  {
    private readonly ICategoryRepository _repository;

    public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();
    public Category SelectedCategory { get; set; }
    public Category CurrentCategory { get; set; } = new Category();

    public CategoryViewModel(ICategoryRepository repository)
    {
      _repository = repository;
      LoadCategories();
    }

    // Синхронне завантаження категорій
    public void LoadCategories()
    {
      try
      {
        Categories.Clear();
        var categories = _repository.GetAll();  // Використовуємо синхронний метод GetAll
        foreach (var category in categories)
        {
          Categories.Add(category);
        }
      }
      catch (Exception ex)
      {
        // Логування помилки
        Console.WriteLine($"Помилка при завантаженні категорій: {ex.Message}");
      }
    }

    public void AddCategory(Category category)
    {
      _repository.Add(category);
      Categories.Add(category);
    }

    public void UpdateCategory(Category category)
    {
      _repository.Update(category);
      var index = Categories.IndexOf(Categories.First(c => c.CategoryID == category.CategoryID));
      Categories[index] = category;
      LoadCategories() ;
    }

    public void DeleteCategory(int id)
    {
      _repository.Delete(_repository.GetById(id));
      Categories.Remove(Categories.First(c => c.CategoryID == id));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
