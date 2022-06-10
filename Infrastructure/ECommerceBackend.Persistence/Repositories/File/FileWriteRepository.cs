using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Persistence.Contexts;
using File = ECommerceBackend.Domain.Entities.File;

namespace ECommerceBackend.Persistence.Repositories;

public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
{
    public FileWriteRepository(ECommerceBackendDbContext context) : base(context)
    {

    }
}