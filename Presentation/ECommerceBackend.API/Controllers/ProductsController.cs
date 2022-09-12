using ECommerceBackend.Application.Features.ProductImageFiles.Commands.DeleteProductImage;
using ECommerceBackend.Application.Features.ProductImageFiles.Commands.UploadProductImages;
using ECommerceBackend.Application.Features.ProductImageFiles.Queries.GetProductImages;
using ECommerceBackend.Application.Features.Products.Commands.CreateProduct;
using ECommerceBackend.Application.Features.Products.Commands.DeleteProduct;
using ECommerceBackend.Application.Features.Products.Commands.UpdateProduct;
using ECommerceBackend.Application.Features.Products.Queries.GetAllProducts;
using ECommerceBackend.Application.Features.Products.Queries.GetProductById;
using ECommerceBackend.Application.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ECommerceBackend.Application.Features.ProductImageFiles.Commands.ChangeShowCaseImage;

namespace ECommerceBackend.API.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters paginationParameters)
        {
            var response = await Mediator!.Send(new GetAllProductsQueryRequest(paginationParameters));
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetProductByIdQueryRequest request)
        {
            var product = await Mediator!.Send(request);
            return Ok(product);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Create(CreateProductCommandRequest request)
        {
            await Mediator!.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
        {
            await Mediator!.Send(request);
            return Ok();
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            await Mediator!.Send(request);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload([FromQuery] string id)
        {
            var request = new UploadProductImagesCommandRequest
            {
                Id = id,
                Files = Request.Form.Files
            };
            await Mediator!.Send(request);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage(string id, [FromQuery] string imageId)
        {
            var request = new DeleteProductImageCommandRequest()
            {
                Id = id,
                ImageId = imageId
            };
            await Mediator!.Send(request);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok();
        }
    }
}