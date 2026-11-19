namespace Lab8_QuispeBrahan.Models.DTOs;

public class ClientOrderCountDto
{
    public int Clientid { get; set; }
    public string ClientName { get; set; } = null!;
    public int OrderCount { get; set; }
}
