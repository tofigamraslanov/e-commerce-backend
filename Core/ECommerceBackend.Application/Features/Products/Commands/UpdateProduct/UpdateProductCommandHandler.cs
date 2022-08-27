using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository,
        ILogger<UpdateProductCommandHandler> logger)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _logger = logger;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Name = request.Name;
        product.Stock = request.Stock;
        product.Price = request.Price;
        await _productWriteRepository.SaveChangesAsync();
        _logger.LogInformation("Product updated");
        return new();
    }
}