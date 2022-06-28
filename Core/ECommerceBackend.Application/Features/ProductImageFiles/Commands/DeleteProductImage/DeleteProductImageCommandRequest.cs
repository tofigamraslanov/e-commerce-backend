using MediatR;

namespace ECommerceBackend.Application.Features.ProductImageFiles.Commands.DeleteProductImage;

public class DeleteProductImageCommandRequest : IRequest<DeleteProductImageCommandResponse>
{
    public string Id { get; set; } = null!;
    public string ImageId { get; set; } = null!;
}