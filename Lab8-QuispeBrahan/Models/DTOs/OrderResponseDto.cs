namespace Lab8_QuispeBrahan.Models.DTOs;

public class OrderResponseDto
{
    public int Orderid { get; set; }
    public int Clientid { get; set; }
    public DateTime Orderdate { get; set; }
    public List<OrderProductDto> Details { get; set; } = new();
}
