namespace ProductCRUD.Application.Common.Dtos;


public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public int Price { get; set; }
    public double Tax { get; set; }
    public DateTime? DateCreated { get; set; }
}
