﻿using ECommerceApp.Domain.Entities.Common;

namespace ECommerceApp.Domain.Entities;

public class Product : BaseEntity
{
    public Product()
    {
        Orders = new HashSet<Order>();
    }

    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }

    public ICollection<Order> Orders { get; private set; }
}