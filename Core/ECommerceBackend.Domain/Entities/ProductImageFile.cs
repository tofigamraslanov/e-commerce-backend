namespace ECommerceBackend.Domain.Entities;

public class ProductImageFile : File
{
    public ProductImageFile()
    {
        Products = new HashSet<Product>();
    }

    public bool Showcase { get; set; }
    public ICollection<Product> Products { get; set; }
}