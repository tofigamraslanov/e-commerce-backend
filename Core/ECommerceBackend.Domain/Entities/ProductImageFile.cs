namespace ECommerceBackend.Domain.Entities;

public class ProductImageFile : File
{
    public ProductImageFile()
    {
        Products = new HashSet<Product>();
    }

    public ICollection<Product> Products { get; set; }
}