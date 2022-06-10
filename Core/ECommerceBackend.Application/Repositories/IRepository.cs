using ECommerceBackend.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Application.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }
}