using Microsoft.AspNetCore.Mvc;
using ORMFundamentals.Entities;
using ORMFundamentals.Services;

namespace ORMFundamentals.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public IEnumerable<Product> GetAll()
    {
        return _productService.GetAllProducts();
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProductById(int id)
    {
        return this._productService.GetProductById(id) ?? new ActionResult<Product>(NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Add(Product product)
    {
        await _productService.Add(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> Update(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        await _productService.Update(product);

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.Delete(id);

        return NoContent();
    }
}
