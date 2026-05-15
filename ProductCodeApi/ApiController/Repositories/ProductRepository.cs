using Microsoft.EntityFrameworkCore;
using ProductCodeApi.ApiController.Models;

namespace ProductCodeApi.ApiController.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Products.AsNoTracking()
            .OrderBy(p => p.Id)
            .Select(p => new ProductResponse { Id = p.Id, PluCode = p.PluCode })
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsByPluCodeAsync(string pluCode, CancellationToken cancellationToken = default) =>
        _db.Products.AsNoTracking().AnyAsync(p => p.PluCode == pluCode, cancellationToken);

    public async Task<ProductResponse> CreateAsync(string pluCode, CancellationToken cancellationToken = default)
    {
        var entity = new Product { PluCode = pluCode };
        _db.Products.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new ProductResponse { Id = entity.Id, PluCode = entity.PluCode };
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _db.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _db.Products.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
