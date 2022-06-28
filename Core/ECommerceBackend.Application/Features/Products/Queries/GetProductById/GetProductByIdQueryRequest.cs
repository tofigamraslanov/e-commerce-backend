using MediatR;

namespace ECommerceBackend.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
{
    public string Id { get; set; } = null!;
}