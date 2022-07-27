using ECommerceBackend.Application.Abstractions.Storage;
using ECommerceBackend.Application.Features.ProductImageFiles.Commands.DeleteProductImage;
using ECommerceBackend.Application.Features.ProductImageFiles.Commands.UploadProductImages;
using ECommerceBackend.Application.Features.ProductImageFiles.Queries.GetProductImages;
using ECommerceBackend.Application.Features.Products.Commands.CreateProduct;
using ECommerceBackend.Application.Features.Products.Commands.DeleteProduct;
using ECommerceBackend.Application.Features.Products.Commands.UpdateProduct;
using ECommerceBackend.Application.Features.Products.Queries.GetAllProducts;
using ECommerceBackend.Application.Features.Products.Queries.GetProductById;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.RequestParameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;
        public ProductsController(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters paginationParameters)
        {
            var response = await _mediator.Send(new GetAllProductsQueryRequest(paginationParameters));
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetProductByIdQueryRequest request)
        {
            var product = await _mediator.Send(request);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] string id)
        {
            var request = new UploadProductImagesCommandRequest
            {
                Id = id,
                Files = Request.Form.Files
            };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, [FromQuery] string imageId)
        {
            var request = new DeleteProductImageCommandRequest()
            {
                Id = id,
                ImageId = imageId
            };
            await _mediator.Send(request);
            return Ok();
        }
    }
}