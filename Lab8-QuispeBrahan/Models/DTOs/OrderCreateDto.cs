using System.ComponentModel.DataAnnotations;

namespace Lab8_QuispeBrahan.Models.DTOs;

public class OrderCreateDto
{
    [Range(1, int.MaxValue)]
    public int Clientid { get; set; }

    public DateTime Orderdate { get; set; } = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified);
}
