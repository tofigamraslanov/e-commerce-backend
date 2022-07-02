namespace ECommerceBackend.Application.Features.ProductImageFiles.Queries.GetProductImages;

public class GetProductImagesQueryResponse
{
    public Guid Id { get; set; }
    public string Path { get; set; } = null!;
    public string? FileName { get; set; } = null!;
}