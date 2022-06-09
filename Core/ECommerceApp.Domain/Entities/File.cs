using System.ComponentModel.DataAnnotations.Schema;
using ECommerceApp.Domain.Entities.Common;

namespace ECommerceApp.Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; } = null!;
    public string Path { get; set; } = null!;

    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
}