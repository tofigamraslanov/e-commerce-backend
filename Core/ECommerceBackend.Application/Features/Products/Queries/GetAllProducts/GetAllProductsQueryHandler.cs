using ECommerceBackend.Application.Repositories;
using MediatR;

namespace ECommerceBackend.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetAllProductsQueryResponse();
        await Task.Run(() =>
        {
            var products = _productReadRepository
                .GetAll(false)
                .Skip(request.PaginationParameters.Size * request.PaginationParameters.Page)
                .Take(request.PaginationParameters.Size)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                }).ToList();

            var productsCount = _productReadRepository.GetAll(false).Count();

            response.Products = products;
            response.ProductsCount = productsCount;
        }, cancellationToken);

        return response;
    }
}