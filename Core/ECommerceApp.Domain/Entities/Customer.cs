using ECommerceApp.Domain.Entities.Common;

namespace ECommerceApp.Domain.Entities;

public class Customer : BaseEntity
{
    public Customer()
    {
        Orders = new HashSet<Order>();
    }

    public string Name { get; set; } = null!;

    public ICollection<Order> Orders { get; set; }
}