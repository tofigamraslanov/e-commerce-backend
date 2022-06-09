using ECommerceApp.Application.Repositories;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Persistence.Contexts;

namespace ECommerceApp.Persistence.Repositories;

public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(ECommerceAppDbContext context) : base(context)
    {

    }
}