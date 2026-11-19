namespace Lab8_QuispeBrahan.Models.DTOs;

public class ClientOrderDto
{
    public string ClientName { get; set; }
    public List<OrderDto> Orders { get; set; }

}