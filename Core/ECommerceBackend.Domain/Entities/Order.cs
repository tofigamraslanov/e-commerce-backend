using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities;

public class Order : BaseEntity
{
    public Order()
    {
        Products = new HashSet<Product>();
    }

    public string Description { get; set; } = null!;
    public string Address { get; set; } = null!;

    public ICollection<Product> Products { get; private set; }

    public Customer Customer { get; set; } = null!;
    public Guid CustomerId { get; set; }
}