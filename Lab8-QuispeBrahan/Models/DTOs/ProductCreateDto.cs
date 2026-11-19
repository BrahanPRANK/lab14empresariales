using System.ComponentModel.DataAnnotations;

namespace Lab8_QuispeBrahan.Models.DTOs;

public class ProductCreateDto
{
    [Required, StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? Description { get; set; }

    [Range(typeof(decimal), "0", "99999999.99", ParseLimitsInInvariantCulture = true)]
    public decimal Price { get; set; }
}
