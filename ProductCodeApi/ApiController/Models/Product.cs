using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCodeApi.ApiController.Models;

[Table("Products")]
public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(35)]
    public string PluCode { get; set; } = string.Empty;
}
