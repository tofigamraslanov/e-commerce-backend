using ECommerceBackend.Application.ViewModels.Products;
using MediatR;

namespace ECommerceBackend.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public float Price { get; set; }
}