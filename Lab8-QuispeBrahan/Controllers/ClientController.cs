using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private readonly IClienteService servicio;

    public ClientController(IClienteService servicio)
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
    public async Task<IActionResult> Agregar(ClientCreateDto datos)
    {
        return await servicio.Agregar(datos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, ClientCreateDto datos)
    {
        return await servicio.Actualizar(id, datos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        return await servicio.Eliminar(id);
    }

    [HttpGet("nombre")]
    public async Task<IActionResult> ClientesConJuan()
    {
        return await servicio.ClientesConJuan();
    }

    [HttpGet("mayorNumeroPedidos")]
    public async Task<IActionResult> ClienteConMasPedidos()
    {
        return await servicio.ClienteConMasPedidos();
    }

    [HttpGet("porProducto")]
    public async Task<IActionResult> ClientesDelProducto2()
    {
        return await servicio.ClientesDelProducto2();
    }
    [HttpGet("totalProductos")]
    public async Task<IActionResult> ClientesConTotalProductos()
    {
        return await servicio.ClientesConTotalProductos();
    }
    
    [HttpGet("clientesConPedidos")]
    public async Task<IActionResult> ConsultaNueva()
    {
        return await servicio.ClientesConPedidos();
    }
}
