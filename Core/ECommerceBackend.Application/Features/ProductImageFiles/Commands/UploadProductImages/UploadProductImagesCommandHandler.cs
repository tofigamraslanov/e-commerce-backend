using ECommerceBackend.Application.Abstractions.Storage;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using MediatR;

namespace ECommerceBackend.Application.Features.ProductImageFiles.Commands.UploadProductImages;

public class UploadProductImagesCommandHandler : IRequestHandler<UploadProductImagesCommandRequest, UploadProductImagesCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IStorageService _storageService;

    public UploadProductImagesCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
    {
        _productReadRepository = productReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _storageService = storageService;
    }

    public async Task<UploadProductImagesCommandResponse> Handle(UploadProductImagesCommandRequest request, CancellationToken cancellationToken)
    {
        var data = await _storageService.UploadAsync("product-images", request.Files);

        var product = await _productReadRepository.GetByIdAsync(request.Id);

        await _productImageFileWriteRepository.AddRangeAsync(data.Select(d => new ProductImageFile
        {
            FileName = d.fileName,
            Path = d.pathOrContainerName,
            Storage = _storageService.StorageName,
            Products = new List<Product> { product }
        }).ToList());
        await _productImageFileWriteRepository.SaveChangesAsync();

        return new();
    }
}