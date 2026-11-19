using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;

namespace Lab8_QuispeBrahan.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> Listar();
    Task<Product?> Buscar(int id);
    Task Agregar(Product producto);
    void Actualizar(Product producto);
    void Eliminar(Product producto);

    Task<List<ProductResponseDto>> ProductosPorPrecio(decimal precio);
    Task<ProductResponseDto?> ProductoMasCaro();
    Task<decimal?> PrecioPromedio();
    Task<List<ProductResponseDto>> ProductosSinDescripcion();
    Task<List<ProductResponseDto>> ProductosDelCliente1();
    Task<bool> TieneDetalles(int id);
}
