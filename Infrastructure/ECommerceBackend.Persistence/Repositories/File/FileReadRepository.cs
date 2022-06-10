using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Persistence.Contexts;
using File = ECommerceBackend.Domain.Entities.File;

namespace ECommerceBackend.Persistence.Repositories;

public class FileReadRepository : ReadRepository<File>, IFileReadRepository
{
    public FileReadRepository(ECommerceBackendDbContext context) : base(context)
    {

    }
}