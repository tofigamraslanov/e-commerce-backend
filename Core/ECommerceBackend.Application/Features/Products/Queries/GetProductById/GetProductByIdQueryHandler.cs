using ECommerceBackend.Application.Repositories;
using MediatR;

namespace ECommerceBackend.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.Id, false);
        return new()
        {
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock
        };
    }
}