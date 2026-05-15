using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ProductCodeApp.Models;

namespace ProductCodeApp.Api;

public sealed class ProductApiClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;

    public ProductApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<ProductRow>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var list = await _http
            .GetFromJsonAsync<List<ProductRow>>("api/Products", JsonOptions, cancellationToken)
            .ConfigureAwait(false);
        return list ?? [];
    }

    public async Task<(bool ok, string? errorMessage)> CreateAsync(
        string pluCode,
        CancellationToken cancellationToken = default)
    {
        using var response = await _http
            .PostAsJsonAsync("api/Products", new { pluCode }, JsonOptions, cancellationToken)
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            return (true, null);
        }

        var msg = await TryReadMessageAsync(response, cancellationToken).ConfigureAwait(false);
        return (false, msg ?? $"เซิร์ฟเวอร์ตอบกลับ {(int)response.StatusCode}");
    }

    public async Task<(bool ok, string? errorMessage)> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        using var response = await _http
            .DeleteAsync($"api/Products/{id}", cancellationToken)
            .ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return (true, null);
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return (false, "ไม่พบข้อมูลที่ต้องการลบ");
        }

        var msg = await TryReadMessageAsync(response, cancellationToken).ConfigureAwait(false);
        return (false, msg ?? $"เซิร์ฟเวอร์ตอบกลับ {(int)response.StatusCode}");
    }

    private static async Task<string?> TryReadMessageAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        try
        {
            var envelope = await response.Content
                .ReadFromJsonAsync<ApiMessageEnvelope>(JsonOptions, cancellationToken)
                .ConfigureAwait(false);
            return envelope?.Message;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private sealed class ApiMessageEnvelope
    {
        public string? Message { get; set; }
    }
}
