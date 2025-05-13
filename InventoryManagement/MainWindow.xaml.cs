using InventoryManagement.Interfaces;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagement
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private readonly IProductRepository _productRepository;

    public MainWindow(IProductRepository productRepository)
    {
      InitializeComponent();
      _productRepository = productRepository;

      // Тепер можна використовувати методи: _productRepository.GetAllAsync(), і т.д.
    }
    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
      var products = await _productRepository.GetAllAsync();

      // Наприклад, якщо у тебе є ListBox або DataGrid
      ProductListBox.ItemsSource = products;
    }

  }

}
