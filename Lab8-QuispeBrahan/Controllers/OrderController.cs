using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService servicio;

    public OrderController(IOrderService servicio)
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
    public async Task<IActionResult> Agregar(OrderCreateDto datos)
    {
        return await servicio.Agregar(datos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, OrderCreateDto datos)
    {
        return await servicio.Actualizar(id, datos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        return await servicio.Eliminar(id);
    }

    [HttpGet("despuesDe")]
    public async Task<IActionResult> PedidosDespuesDeMayo()
    {
        return await servicio.PedidosDespuesDeMayo();
    }

    [HttpGet("conDetalles")]
    public async Task<IActionResult> PedidosConDetalles()
    {
        return await servicio.PedidosConDetalles();
    }
    
    [HttpGet("pedidosConProductos")]
    public async Task<IActionResult> PedidosConProductos()
    {
        return await servicio.PedidosConProductos();
    }
    
    [HttpGet("ventasPorCliente")]
    public async Task<IActionResult> VentasPorCliente()
    {
        return await servicio.VentasPorCliente();
    }
}
