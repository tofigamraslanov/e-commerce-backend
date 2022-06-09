using ECommerceApp.Application.Repositories;
using ECommerceApp.Persistence.Contexts;
using File = ECommerceApp.Domain.Entities.File;

namespace ECommerceApp.Persistence.Repositories;

public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
{
    public FileWriteRepository(ECommerceAppDbContext context) : base(context)
    {

    }
}