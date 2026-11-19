using Lab8_QuispeBrahan.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Services.Interfaces;

public interface IClienteService
{
    Task<IActionResult> Listar();
    Task<IActionResult> Buscar(int id);
    Task<IActionResult> Agregar(ClientCreateDto datos);
    Task<IActionResult> Actualizar(int id, ClientCreateDto datos);
    Task<IActionResult> Eliminar(int id);
    Task<IActionResult> ClientesConJuan();
    Task<IActionResult> ClienteConMasPedidos();
    Task<IActionResult> ClientesDelProducto2();
    Task<IActionResult> ClientesConTotalProductos();
    Task<IActionResult> ClientesConPedidos();
}
