using ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryResponse
{
    public object Products { get; set; } = null!;
    public int ProductsCount { get; set; }
}