using ECommerceApp.Application.Repositories;
using ECommerceApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new List<Product>
            {
                new() { Id = Guid.NewGuid(), Name = "Product 1",CreatedDate = DateTime.UtcNow, Price = 100, Stock = 10},
                new() { Id = Guid.NewGuid(),Name = "Product 2",CreatedDate = DateTime.UtcNow,Price = 200,Stock = 20},
                new() { Id = Guid.NewGuid(),Name = "Product 3",CreatedDate = DateTime.UtcNow,Price = 300,Stock = 30},
            });
            await _productWriteRepository.SaveChangesAsync();
        }
    }
}
