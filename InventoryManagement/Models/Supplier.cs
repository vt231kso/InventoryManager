namespace InventoryManagement.Models
{
  public class Supplier
  {
    public int SupplierID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
  }
}
