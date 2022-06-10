using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.RequestParameters;
using ECommerceBackend.Application.Services;
using ECommerceBackend.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using File = ECommerceBackend.Domain.Entities.File;

// Test api
namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        [HttpGet]
        public Task<IActionResult> GetAll([FromQuery] PaginationParameters paginationParameters)
        {
            var products = _productReadRepository
                .GetAll(false)
                .Skip(paginationParameters.Size * paginationParameters.Page)
                .Take(paginationParameters.Size)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                }).ToList();

            var productsCount = _productReadRepository.GetAll(false).Count();

            return Task.FromResult<IActionResult>(Ok(new
            {
                products,
                productsCount
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm createProductVm)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = createProductVm.Name,
                Stock = createProductVm.Stock,
                Price = createProductVm.Price
            });
            await _productWriteRepository.SaveChangesAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductVm updateProductVm)
        {
            var product = await _productReadRepository.GetByIdAsync(updateProductVm.Id);
            product.Name = updateProductVm.Name;
            product.Stock = updateProductVm.Stock;
            product.Price = updateProductVm.Price;
            await _productWriteRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var data = await _fileService.UploadAsync("uploads/files", Request.Form.Files);

            // await _productImageFileWriteRepository.AddRangeAsync(data.Select(d => new ProductImageFile
            // {
            //     FileName = d.fileName,
            //     Path = d.path
            // }).ToList());
            // await _productImageFileWriteRepository.SaveChangesAsync();

            // await _invoiceFileWriteRepository.AddRangeAsync(data.Select(d => new InvoiceFile()
            // {
            //     FileName = d.fileName,
            //     Path = d.path,
            //     Price = new Random().Next()
            // }).ToList());
            // await _invoiceFileWriteRepository.SaveChangesAsync();

            await _fileWriteRepository.AddRangeAsync(data.Select(d => new File()
            {
                FileName = d.fileName,
                Path = d.path,
            }).ToList());
            await _fileWriteRepository.SaveChangesAsync();

            // var d = _fileReadRepository.GetAll();
            // var d2 = _invoiceFileReadRepository.GetAll();
            // var d3 = _productImageFileReadRepository.GetAll();

            return Ok();
        }
    }
}