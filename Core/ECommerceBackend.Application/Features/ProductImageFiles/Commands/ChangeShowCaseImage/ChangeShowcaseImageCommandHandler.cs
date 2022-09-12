using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Application.Features.ProductImageFiles.Commands.ChangeShowCaseImage;

public class
    ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest,
        ChangeShowcaseImageCommandResponse>
{
    private readonly IProductImageFileWriteRepository _repository;

    public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request,
        CancellationToken cancellationToken)
    {
        var query = _repository.Table
            .Include(p => p.Products)
            .SelectMany(p => p.Products, (productImageFiles, products) => new
            {
                productImageFiles,
                products
            });

        var data = await query.FirstOrDefaultAsync(p =>
                p.products.Id == Guid.Parse(request.ProductId) && p.productImageFiles.Showcase == true,
            cancellationToken: cancellationToken);

        if (data is not null)
            data.productImageFiles.Showcase = false;

        var result = await query.FirstOrDefaultAsync(p => p.productImageFiles.Id == Guid.Parse(request.ImageId),
            cancellationToken: cancellationToken);
        if (result is not null)
            result.productImageFiles.Showcase = true;

        await _repository.SaveChangesAsync();

        return new ChangeShowcaseImageCommandResponse();
    }
}