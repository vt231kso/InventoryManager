using System.Collections.ObjectModel;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using System.ComponentModel;
using InventoryManagement.ViewModels.Sorting;
using InventoryManagement.Services;

using System.Linq;

namespace InventoryManagement.ViewModels
{
    public enum SortCriterion { Name, Price, Quantity }

    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly InventoryStatisticsService _statisticsService;
        private ProductSorter _sorter = new ProductSorter(new SortByNameStrategy());

        public ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();
        public ObservableCollection<Supplier> Suppliers { get; } = new ObservableCollection<Supplier>();

        public int TotalQuantity => _statisticsService.GetTotalQuantity(Products);
        public decimal TotalValue => _statisticsService.GetTotalValue(Products);

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                if (value != null)
                    CurrentProduct = new Product(value); // передбачає наявність конструктора копіювання
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
            ISupplierRepository supplierRepository,
            InventoryStatisticsService statisticsService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _statisticsService = statisticsService;

            LoadData();
        }

        public void LoadData()
        {
            Products = new ObservableCollection<Product>(_productRepository.GetAll());
            OnPropertyChanged(nameof(Products));

            Categories.Clear();
            foreach (var category in _categoryRepository.GetAll())
                Categories.Add(category);

            Suppliers.Clear();
            foreach (var supplier in _supplierRepository.GetAll())
                Suppliers.Add(supplier);

            RefreshStatistics();
        }

        public void SortProducts(SortCriterion criterion)
        {
            switch (criterion)
            {
                case SortCriterion.Name:
                    _sorter.SetStrategy(new SortByNameStrategy());
                    break;
                case SortCriterion.Price:
                    _sorter.SetStrategy(new SortByPriceStrategy());
                    break;
                case SortCriterion.Quantity:
                    _sorter.SetStrategy(new SortByQuantityStrategy());
                    break;
            }


            Products = new ObservableCollection<Product>(_sorter.Sort(Products.ToList()));
            OnPropertyChanged(nameof(Products));
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
            var product = _productRepository.GetById(id);
            _productRepository.Delete(product);
            Products.Remove(product);
            RefreshStatistics();
        }

        private void RefreshStatistics()
        {
            OnPropertyChanged(nameof(TotalQuantity));
            OnPropertyChanged(nameof(TotalValue));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
