using Lab8_QuispeBrahan.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Services.Interfaces;

public interface IOrderDetailService
{
    Task<IActionResult> Listar();
    Task<IActionResult> Buscar(int id);
    Task<IActionResult> Agregar(OrderDetailCreateDto datos);
    Task<IActionResult> Actualizar(int id, OrderDetailCreateDto datos);
    Task<IActionResult> Eliminar(int id);
    Task<IActionResult> ProductosDelPedido1();
    Task<IActionResult> CantidadDelPedido1();
}
