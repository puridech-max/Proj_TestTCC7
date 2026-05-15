using ProductCodeApi.ApiController.Models;

namespace ProductCodeApi.ApiController.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsByPluCodeAsync(string pluCode, CancellationToken cancellationToken = default);

    Task<ProductResponse> CreateAsync(string pluCode, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
