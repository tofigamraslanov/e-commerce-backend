using ECommerceApp.Application.Repositories;
using ECommerceApp.Persistence.Contexts;
using File = ECommerceApp.Domain.Entities.File;

namespace ECommerceApp.Persistence.Repositories;

public class FileReadRepository : ReadRepository<File>, IFileReadRepository
{
    public FileReadRepository(ECommerceAppDbContext context) : base(context)
    {

    }
}