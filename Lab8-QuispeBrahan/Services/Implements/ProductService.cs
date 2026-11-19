using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Services.Implements;

public class ProductService : IProductService
{
    private readonly IUnitOfWork unidad;

    public ProductService(IUnitOfWork unidad)
    {
        this.unidad = unidad;
    }

    public async Task<IActionResult> Listar()
    {
        try
        {
            var registros = await unidad.Productos.Listar();
            var lista = new List<ProductResponseDto>();
            foreach (var producto in registros)
            {
                lista.Add(Convertir(producto));
            }
            return new OkObjectResult(lista);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Buscar(int id)
    {
        try
        {
            var producto = await unidad.Productos.Buscar(id);
            if (producto == null)
                return new NotFoundResult();
            return new OkObjectResult(Convertir(producto));
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Agregar(ProductCreateDto datos)
    {
        try
        {
            var producto = new Product { Name = datos.Name, Description = datos.Description, Price = datos.Price };
            await unidad.Productos.Agregar(producto);
            await unidad.GuardarCambios();
            return new CreatedResult($"/api/products/{producto.Productid}", Convertir(producto));
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Actualizar(int id, ProductCreateDto datos)
    {
        try
        {
            var producto = await unidad.Productos.Buscar(id);
            if (producto == null)
                return new NotFoundResult();
            producto.Name = datos.Name;
            producto.Description = datos.Description;
            producto.Price = datos.Price;
            unidad.Productos.Actualizar(producto);
            await unidad.GuardarCambios();
            return new NoContentResult();
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        try
        {
            var producto = await unidad.Productos.Buscar(id);
            if (producto == null)
                return new NotFoundResult();
            if (await unidad.Productos.TieneDetalles(id))
                return new ConflictObjectResult(new { mensaje = "No se puede eliminar: tiene detalles asociados." });
            unidad.Productos.Eliminar(producto);
            await unidad.GuardarCambios();
            return new NoContentResult();
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> ProductosPorPrecio(decimal? precio)
    {
        try
        {
            if (precio == null || precio < 0)
                return new BadRequestObjectResult(new { mensaje = "Ingresa un precio mayor o igual a cero." });
            var resultado = await unidad.Productos.ProductosPorPrecio(precio.Value);
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> ProductoMasCaro()
    {
        try
        {
            var resultado = await unidad.Productos.ProductoMasCaro();
            if (resultado == null)
                return new NotFoundResult();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> PrecioPromedio()
    {
        try
        {
            var resultado = await unidad.Productos.PrecioPromedio();
            return new OkObjectResult(new { promedio = resultado });
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> ProductosSinDescripcion()
    {
        try
        {
            var resultado = await unidad.Productos.ProductosSinDescripcion();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> ProductosDelCliente1()
    {
        try
        {
            var resultado = await unidad.Productos.ProductosDelCliente1();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    private static ProductResponseDto Convertir(Product producto)
    {
        return new ProductResponseDto
        {
            Productid = producto.Productid,
            Name = producto.Name,
            Description = producto.Description,
            Price = producto.Price
        };
    }

}
