using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities;

public class Product : BaseEntity
{
    public Product()
    {
        Orders = new HashSet<Order>();
    }

    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public float Price { get; set; }

    public ICollection<Order> Orders { get; private set; }
}