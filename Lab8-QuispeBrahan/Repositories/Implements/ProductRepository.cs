using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8_QuispeBrahan.Repositories.Implements;

public class ProductRepository : IProductRepository
{
    private readonly Laboratorio8Context contexto;

    public ProductRepository(Laboratorio8Context contexto)
    {
        this.contexto = contexto;
    }

    public async Task<List<Product>> Listar()
    {
        return await contexto.Products.ToListAsync();
    }

    public async Task<Product?> Buscar(int id)
    {
        return await contexto.Products.FindAsync(id);
    }

    public async Task Agregar(Product producto)
    {
        await contexto.Products.AddAsync(producto);
    }

    public void Actualizar(Product producto)
    {
        contexto.Products.Update(producto);
    }

    public void Eliminar(Product producto)
    {
        contexto.Products.Remove(producto);
    }

    public async Task<List<ProductResponseDto>> ProductosPorPrecio(decimal precio)
    {
        var productos = await contexto.Products
            .Where(producto => producto.Price > precio)
            .Select(producto => new ProductResponseDto
            {
                Productid = producto.Productid, Name = producto.Name,
                Description = producto.Description, Price = producto.Price
            }).ToListAsync();
        return productos;
    }

    public async Task<ProductResponseDto?> ProductoMasCaro()
    {
        var producto = await contexto.Products
            .OrderByDescending(producto => producto.Price)
            .ThenBy(producto => producto.Productid)
            .Select(producto => new ProductResponseDto
            {
                Productid = producto.Productid, Name = producto.Name,
                Description = producto.Description, Price = producto.Price
            }).FirstOrDefaultAsync();
        return producto;
    }

    public async Task<decimal?> PrecioPromedio()
    {
        var promedio = await contexto.Products
            .Select(producto => (decimal?)producto.Price)
            .AverageAsync();
        return promedio;
    }

    public async Task<List<ProductResponseDto>> ProductosSinDescripcion()
    {
        var productos = await contexto.Products
            .Where(producto => producto.Description == null || producto.Description == "")
            .Select(producto => new ProductResponseDto
            {
                Productid = producto.Productid, Name = producto.Name,
                Description = producto.Description, Price = producto.Price
            }).ToListAsync();
        return productos;
    }

    public async Task<List<ProductResponseDto>> ProductosDelCliente1()
    {
        var productos = await contexto.Orderdetails
            .Where(detalle => detalle.Order.Clientid == 1)
            .Select(detalle => new ProductResponseDto
            {
                Productid = detalle.Productid, Name = detalle.Product.Name,
                Description = detalle.Product.Description, Price = detalle.Product.Price
            }).Distinct().ToListAsync();
        return productos;
    }

    public async Task<bool> TieneDetalles(int id)
    {
        return await contexto.Orderdetails.AnyAsync(detalle => detalle.Productid == id);
    }
}
