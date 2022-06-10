using System.ComponentModel.DataAnnotations.Schema;
using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string Storage { get; set; } = null!;

    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
}