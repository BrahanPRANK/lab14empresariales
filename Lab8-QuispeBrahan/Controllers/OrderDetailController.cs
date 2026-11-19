using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Controllers;

[ApiController]
[Route("api/orderdetails")]
public class OrderDetailController : ControllerBase
{
    private readonly IOrderDetailService servicio;

    public OrderDetailController(IOrderDetailService servicio)
    {
        this.servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return await servicio.Listar();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
        return await servicio.Buscar(id);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar(OrderDetailCreateDto datos)
    {
        return await servicio.Agregar(datos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, OrderDetailCreateDto datos)
    {
        return await servicio.Actualizar(id, datos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        return await servicio.Eliminar(id);
    }

    [HttpGet("porPedido")]
    public async Task<IActionResult> ProductosDelPedido1()
    {
        return await servicio.ProductosDelPedido1();
    }

    [HttpGet("porPedidoCantidadTotal")]
    public async Task<IActionResult> CantidadDelPedido1()
    {
        return await servicio.CantidadDelPedido1();
    }
}
