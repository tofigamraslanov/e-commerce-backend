namespace ECommerceBackend.Application.ViewModels.Products;

public class UpdateProductVm
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public float Price { get; set; }
}