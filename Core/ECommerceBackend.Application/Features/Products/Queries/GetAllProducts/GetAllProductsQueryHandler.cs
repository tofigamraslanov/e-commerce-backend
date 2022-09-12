using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly ILogger<GetAllProductsQueryHandler> _logger;

    public GetAllProductsQueryHandler(IProductReadRepository productReadRepository,
        ILogger<GetAllProductsQueryHandler> logger)
    {
        _productReadRepository = productReadRepository;
        _logger = logger;
    }

    public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get all products");

        var response = new GetAllProductsQueryResponse();
        await Task.Run(() =>
        {
            var products = _productReadRepository
                .GetAll(false)
                .Skip(request.PaginationParameters.Size * request.PaginationParameters.Page)
                .Take(request.PaginationParameters.Size)
                .Include(p => p.ProductImageFiles)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate,
                    p.ProductImageFiles
                }).ToList();

            var productsCount = _productReadRepository.GetAll(false).Count();

            response.Products = products;
            response.ProductsCount = productsCount;
        }, cancellationToken);

        return response;
    }
}