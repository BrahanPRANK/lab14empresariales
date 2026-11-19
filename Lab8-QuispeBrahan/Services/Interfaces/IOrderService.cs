using Lab8_QuispeBrahan.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Services.Interfaces;

public interface IOrderService
{
    Task<IActionResult> Listar();
    Task<IActionResult> Buscar(int id);
    Task<IActionResult> Agregar(OrderCreateDto datos);
    Task<IActionResult> Actualizar(int id, OrderCreateDto datos);
    Task<IActionResult> Eliminar(int id);
    Task<IActionResult> PedidosDespuesDeMayo();
    Task<IActionResult> PedidosConDetalles();
    Task<IActionResult> PedidosConProductos();
    Task<IActionResult> VentasPorCliente();
}
