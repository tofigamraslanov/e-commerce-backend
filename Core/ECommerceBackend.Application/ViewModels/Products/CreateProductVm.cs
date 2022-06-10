namespace ECommerceBackend.Application.ViewModels.Products;

public class CreateProductVm
{
    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public float Price { get; set; }
}