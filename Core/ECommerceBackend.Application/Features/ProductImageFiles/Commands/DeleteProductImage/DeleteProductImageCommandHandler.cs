using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Application.Features.ProductImageFiles.Commands.DeleteProductImage;

public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id), cancellationToken: cancellationToken);

        var productImage = product!.ProductImageFiles.FirstOrDefault(i => i.Id == Guid.Parse(request.ImageId));

        product.ProductImageFiles.Remove(productImage!);
        await _productWriteRepository.SaveChangesAsync();

        return new();
    }
}