using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities;

public class Order : BaseEntity
{
    public Order()
    {
        Products = new HashSet<Product>();
    }

    public string? Description { get; set; }  
    public string? Address { get; set; }  

    public ICollection<Product> Products { get; set; }

    public Customer? Customer { get; set; }  
    public Guid CustomerId { get; set; }
}