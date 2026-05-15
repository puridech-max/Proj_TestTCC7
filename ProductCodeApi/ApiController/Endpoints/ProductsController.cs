using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCodeApi.ApiController.Models;
using ProductCodeApi.ApiController.Repositories;

namespace ProductCodeApi.ApiController.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _products;

    public ProductsController(IProductRepository products)
    {
        _products = products;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var list = await _products.GetAllAsync(cancellationToken);
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        if (!ProductCodeValidator.TryNormalize(request.PluCode, out var plu, out var err))
        {
            return BadRequest(new { message = err });
        }

        if (await _products.ExistsByPluCodeAsync(plu, cancellationToken))
        {
            return Conflict(new { message = "รหัสสินค้านี้มีอยู่แล้ว ไม่อนุญาตให้ซ้ำ" });
        }

        try
        {
            var dto = await _products.CreateAsync(plu, cancellationToken);
            return Created($"/api/Products/{dto.Id}", dto);
        }
        catch (DbUpdateException)
        {
            return Conflict(new { message = "รหัสสินค้านี้มีอยู่แล้ว ไม่อนุญาตให้ซ้ำ" });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _products.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
