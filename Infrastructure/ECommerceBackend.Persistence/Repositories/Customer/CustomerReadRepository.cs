using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories;

public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(ECommerceBackendDbContext context) : base(context)
    {

    }
}