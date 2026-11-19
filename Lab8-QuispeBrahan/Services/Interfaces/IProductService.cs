using Lab8_QuispeBrahan.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Services.Interfaces;

public interface IProductService
{
    Task<IActionResult> Listar();
    Task<IActionResult> Buscar(int id);
    Task<IActionResult> Agregar(ProductCreateDto datos);
    Task<IActionResult> Actualizar(int id, ProductCreateDto datos);
    Task<IActionResult> Eliminar(int id);
    Task<IActionResult> ProductosPorPrecio(decimal? precio);
    Task<IActionResult> ProductoMasCaro();
    Task<IActionResult> PrecioPromedio();
    Task<IActionResult> ProductosSinDescripcion();
    Task<IActionResult> ProductosDelCliente1();
}
