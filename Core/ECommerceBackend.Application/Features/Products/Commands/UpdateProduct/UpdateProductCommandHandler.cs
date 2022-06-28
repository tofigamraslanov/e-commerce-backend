using ECommerceBackend.Application.Repositories;
using MediatR;

namespace ECommerceBackend.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;

    public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Name = request.Name;
        product.Stock = request.Stock;
        product.Price = request.Price;
        await _productWriteRepository.SaveChangesAsync();
        return new();
    }
}