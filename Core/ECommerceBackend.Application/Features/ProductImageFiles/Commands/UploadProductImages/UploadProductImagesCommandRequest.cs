using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerceBackend.Application.Features.ProductImageFiles.Commands.UploadProductImages;

public class UploadProductImagesCommandRequest : IRequest<UploadProductImagesCommandResponse>
{
    public string Id { get; set; } = null!;
    public IFormFileCollection Files { get; set; } = null!;
}