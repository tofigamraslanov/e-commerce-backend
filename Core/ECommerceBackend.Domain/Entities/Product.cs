using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities;

public class Product : BaseEntity
{
    public Product()
    {
        Orders = new HashSet<Order>();
        ProductImageFiles = new HashSet<ProductImageFile>();
    }

    public string? Name { get; set; }  
    public int Stock { get; set; }
    public float Price { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<ProductImageFile> ProductImageFiles { get; set; }
}