namespace Lab8_QuispeBrahan.Models.DTOs;

public class OrderProductDto
{
    public int Productid { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
}
