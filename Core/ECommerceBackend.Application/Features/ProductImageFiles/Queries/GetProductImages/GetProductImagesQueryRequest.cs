using MediatR;

namespace ECommerceBackend.Application.Features.ProductImageFiles.Queries.GetProductImages;

public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
{
    public string Id { get; set; } = null!;
}