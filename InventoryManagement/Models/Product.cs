namespace InventoryManagement.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }

        // Конструктор копіювання
        public Product(Product other)
        {
            ProductID = other.ProductID;
            Name = other.Name;
            Description = other.Description;
            CategoryID = other.CategoryID;
            SupplierID = other.SupplierID;
            Price = other.Price;
            Quantity = other.Quantity;
            Category = other.Category;
            Supplier = other.Supplier;
        }
        public Product() { }
    }
}
