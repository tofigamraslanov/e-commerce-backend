using ECommerceApp.Application.Repositories;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Persistence.Contexts;

namespace ECommerceApp.Persistence.Repositories;

public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(ECommerceAppDbContext context) : base(context)
    {

    }
}