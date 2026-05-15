namespace ProductCodeApi.ApiController.Models;

public sealed class ProductResponse
{
    public int Id { get; set; }
    public string PluCode { get; set; } = string.Empty;
}

public sealed class CreateProductRequest
{
    public string? PluCode { get; set; }
}
