using System.Net;
using ECommerceApp.Application.Repositories;
using ECommerceApp.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

// Test api
namespace ECommerceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return Task.FromResult<IActionResult>(Ok(_productReadRepository.GetAll(false)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm createProductVm)
        {
            if (ModelState.IsValid)
            {
                
            }
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
    }
}
