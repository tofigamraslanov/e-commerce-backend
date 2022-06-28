using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceBackend.Application.Features.ProductImageFiles.Queries.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IConfiguration _configuration;
    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        _configuration = configuration;
    }

    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id), cancellationToken: cancellationToken);

        return product!.ProductImageFiles.Select(p => new GetProductImagesQueryResponse()
        {
            Id = p.Id,
            Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
            FileName = p.FileName
        }).ToList();
    }
}