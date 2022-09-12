using MediatR;

namespace ECommerceBackend.Application.Features.ProductImageFiles.Commands.ChangeShowCaseImage;

public class ChangeShowcaseImageCommandRequest : IRequest<ChangeShowcaseImageCommandResponse>
{
    public string ImageId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
}