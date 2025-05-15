using Microsoft.Win32;
using System.Linq;
using System.Windows;
using InventoryManagement.ViewModels;
using InventoryManagement.Reports; // Простір імен, де твоя стратегія

namespace InventoryManagement.Views
{

  public partial class AnalyticsView : Window
  {
    private AnalyticsViewModel _analyticsViewModel => DataContext as AnalyticsViewModel;

    public AnalyticsView()
    {
      InitializeComponent();
    }
  }
}
