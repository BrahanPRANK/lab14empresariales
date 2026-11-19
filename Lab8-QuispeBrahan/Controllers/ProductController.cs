using System.ComponentModel.DataAnnotations;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService servicio;

    public ProductController(IProductService servicio)
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
    public async Task<IActionResult> Agregar(ProductCreateDto datos)
    {
        return await servicio.Agregar(datos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, ProductCreateDto datos)
    {
        return await servicio.Actualizar(id, datos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        return await servicio.Eliminar(id);
    }

    [HttpGet("precioMayor")]
    public async Task<IActionResult> ProductosPorPrecio([FromQuery, Required] decimal? precio)
    {
        return await servicio.ProductosPorPrecio(precio);
    }

    [HttpGet("masCaro")]
    public async Task<IActionResult> ProductoMasCaro()
    {
        return await servicio.ProductoMasCaro();
    }

    [HttpGet("precioPpromedio")]
    public async Task<IActionResult> PrecioPromedio()
    {
        return await servicio.PrecioPromedio();
    }

    [HttpGet("sinDescripcion")]
    public async Task<IActionResult> ProductosSinDescripcion()
    {
        return await servicio.ProductosSinDescripcion();
    }

    [HttpGet("porCliente")]
    public async Task<IActionResult> ProductosDelCliente1()
    {
        return await servicio.ProductosDelCliente1();
    }
}
