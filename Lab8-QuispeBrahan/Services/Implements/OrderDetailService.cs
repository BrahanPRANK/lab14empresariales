using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Services.Implements;

public class OrderDetailService : IOrderDetailService
{
    private readonly IUnitOfWork unidad;

    public OrderDetailService(IUnitOfWork unidad)
    {
        this.unidad = unidad;
    }

    public async Task<IActionResult> Listar()
    {
        try
        {
            var registros = await unidad.Detalles.Listar();
            var lista = new List<OrderDetailResponseDto>();
            foreach (var detalle in registros)
            {
                lista.Add(Convertir(detalle));
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
            var detalle = await unidad.Detalles.Buscar(id);
            if (detalle == null)
                return new NotFoundResult();
            return new OkObjectResult(Convertir(detalle));
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Agregar(OrderDetailCreateDto datos)
    {
        try
        {
            if (await unidad.Pedidos.Buscar(datos.Orderid) == null)
                return new BadRequestObjectResult(new { mensaje = "El pedido no existe." });
            if (await unidad.Productos.Buscar(datos.Productid) == null)
                return new BadRequestObjectResult(new { mensaje = "El producto no existe." });
            var detalle = new Orderdetail { Orderid = datos.Orderid, Productid = datos.Productid, Quantity = datos.Quantity };
            await unidad.Detalles.Agregar(detalle);
            await unidad.GuardarCambios();
            return new CreatedResult($"/api/orderdetails/{detalle.Orderdetailid}", Convertir(detalle));
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Actualizar(int id, OrderDetailCreateDto datos)
    {
        try
        {
            var detalle = await unidad.Detalles.Buscar(id);
            if (detalle == null)
                return new NotFoundResult();
            if (await unidad.Pedidos.Buscar(datos.Orderid) == null)
                return new BadRequestObjectResult(new { mensaje = "El pedido no existe." });
            if (await unidad.Productos.Buscar(datos.Productid) == null)
                return new BadRequestObjectResult(new { mensaje = "El producto no existe." });
            detalle.Orderid = datos.Orderid;
            detalle.Productid = datos.Productid;
            detalle.Quantity = datos.Quantity;
            unidad.Detalles.Actualizar(detalle);
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
            var detalle = await unidad.Detalles.Buscar(id);
            if (detalle == null)
                return new NotFoundResult();
            unidad.Detalles.Eliminar(detalle);
            await unidad.GuardarCambios();
            return new NoContentResult();
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> ProductosDelPedido1()
    {
        try
        {
            var resultado = await unidad.Detalles.ProductosDelPedido1();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> CantidadDelPedido1()
    {
        try
        {
            var resultado = await unidad.Detalles.CantidadDelPedido1();
            return new OkObjectResult(new { pedido = 1, cantidadTotal = resultado });
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    private static OrderDetailResponseDto Convertir(Orderdetail detalle)
    {
        return new OrderDetailResponseDto
        {
            Orderdetailid = detalle.Orderdetailid,
            Orderid = detalle.Orderid,
            Productid = detalle.Productid,
            Quantity = detalle.Quantity
        };
    }

}
