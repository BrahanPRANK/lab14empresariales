using System.ComponentModel.DataAnnotations;

namespace Lab8_QuispeBrahan.Models.DTOs;

public class OrderDetailCreateDto
{
    [Range(1, int.MaxValue)]
    public int Orderid { get; set; }

    [Range(1, int.MaxValue)]
    public int Productid { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
