namespace InventoryManagement.Models
{
  public class Product
  {
    public int ProductID { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CategoryID { get; set; }
    public int SupplierID { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Навігаційні властивості (опційно для EF Core)
    public Category? Category { get; set; }
    public Supplier? Supplier { get; set; }
  }
}
