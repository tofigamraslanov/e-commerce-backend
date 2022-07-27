using MediatR;

namespace ECommerceBackend.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
{
    public string Id { get; set; } = null!;
}