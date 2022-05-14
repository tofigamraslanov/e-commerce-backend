using ECommerceApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Application.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }
}