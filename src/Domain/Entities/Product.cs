namespace ProductCRUD.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
    public double Tax { get; set; }
    public string SupplierName { get; set; } = string.Empty;
}
