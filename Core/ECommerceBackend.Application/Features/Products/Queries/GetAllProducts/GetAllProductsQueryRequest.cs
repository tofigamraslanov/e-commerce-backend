using ECommerceBackend.Application.RequestParameters;
using MediatR;

namespace ECommerceBackend.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryRequest : IRequest<GetAllProductsQueryResponse>
{
    public PaginationParameters PaginationParameters { get; set; }

    public GetAllProductsQueryRequest(PaginationParameters paginationParameters)
    {
        PaginationParameters = paginationParameters;
    }
}

