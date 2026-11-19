using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Services.Implements;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork unidad;

    public OrderService(IUnitOfWork unidad)
    {
        this.unidad = unidad;
    }

    public async Task<IActionResult> Listar()
    {
        try
        {
            var registros = await unidad.Pedidos.Listar();
            var lista = new List<OrderResponseDto>();
            foreach (var pedido in registros)
            {
                lista.Add(Convertir(pedido));
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
            var pedido = await unidad.Pedidos.Buscar(id);
            if (pedido == null)
                return new NotFoundResult();
            return new OkObjectResult(Convertir(pedido));
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Agregar(OrderCreateDto datos)
    {
        try
        {
            if (await unidad.Clientes.Buscar(datos.Clientid) == null)
                return new BadRequestObjectResult(new { mensaje = "El cliente no existe." });
            if (datos.Orderdate == default || datos.Orderdate.Kind != DateTimeKind.Unspecified)
                return new BadRequestObjectResult(new { mensaje = "Escribe una fecha sin Z ni zona horaria." });
            var pedido = new Order { Clientid = datos.Clientid, Orderdate = datos.Orderdate };
            await unidad.Pedidos.Agregar(pedido);
            await unidad.GuardarCambios();
            return new CreatedResult($"/api/orders/{pedido.Orderid}", Convertir(pedido));
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Actualizar(int id, OrderCreateDto datos)
    {
        try
        {
            var pedido = await unidad.Pedidos.Buscar(id);
            if (pedido == null)
                return new NotFoundResult();
            if (await unidad.Clientes.Buscar(datos.Clientid) == null)
                return new BadRequestObjectResult(new { mensaje = "El cliente no existe." });
            if (datos.Orderdate == default || datos.Orderdate.Kind != DateTimeKind.Unspecified)
                return new BadRequestObjectResult(new { mensaje = "Escribe una fecha sin Z ni zona horaria." });
            pedido.Clientid = datos.Clientid;
            pedido.Orderdate = datos.Orderdate;
            unidad.Pedidos.Actualizar(pedido);
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
            var pedido = await unidad.Pedidos.Buscar(id);
            if (pedido == null)
                return new NotFoundResult();
            if (await unidad.Pedidos.TieneDetalles(id))
                return new ConflictObjectResult(new { mensaje = "No se puede eliminar: tiene detalles asociados." });
            unidad.Pedidos.Eliminar(pedido);
            await unidad.GuardarCambios();
            return new NoContentResult();
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> PedidosDespuesDeMayo()
    {
        try
        {
            var resultado = await unidad.Pedidos.PedidosDespuesDeMayo();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> PedidosConDetalles()
    {
        try
        {
            var resultado = await unidad.Pedidos.PedidosConDetalles();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    private static OrderResponseDto Convertir(Order pedido)
    {
        return new OrderResponseDto
        {
            Orderid = pedido.Orderid,
            Clientid = pedido.Clientid,
            Orderdate = pedido.Orderdate
        };
    }
    public async Task<IActionResult> PedidosConProductos()
    {
        try
        {
            var pedidos = await unidad.Pedidos.PedidosConProductos();
            return new OkObjectResult(pedidos);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.")
            {
                StatusCode = 500
            };
        }
    }
    
    public async Task<IActionResult> VentasPorCliente()
    {
        try
        {
            var ventas = await unidad.Pedidos.VentasPorCliente();
            return new OkObjectResult(ventas);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.")
            {
                StatusCode = 500
            };
        }
    }

}
