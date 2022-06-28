using MediatR;

namespace ECommerceBackend.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public float Price { get; set; }
}