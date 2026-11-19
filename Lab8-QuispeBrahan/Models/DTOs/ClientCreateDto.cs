using System.ComponentModel.DataAnnotations;

namespace Lab8_QuispeBrahan.Models.DTOs;

public class ClientCreateDto
{
    [Required, StringLength(100)]
    public string Name { get; set; } = null!;

    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = null!;
}
