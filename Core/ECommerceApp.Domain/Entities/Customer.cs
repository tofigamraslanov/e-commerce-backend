using ECommerceApp.Domain.Entities.Common;

namespace ECommerceApp.Domain.Entities;

public class Customer : BaseEntity
{
    public Customer()
    {
        Orders = new HashSet<Order>();
    }

    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}