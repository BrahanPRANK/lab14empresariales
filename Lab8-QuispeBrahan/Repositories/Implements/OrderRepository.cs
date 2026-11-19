using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8_QuispeBrahan.Repositories.Implements;

public class OrderRepository : IOrderRepository
{
    private readonly Laboratorio8Context contexto;

    public OrderRepository(Laboratorio8Context contexto)
    {
        this.contexto = contexto;
    }

    public async Task<List<Order>> Listar()
    {
        return await contexto.Orders.ToListAsync();
    }

    public async Task<Order?> Buscar(int id)
    {
        return await contexto.Orders.FindAsync(id);
    }

    public async Task Agregar(Order pedido)
    {
        await contexto.Orders.AddAsync(pedido);
    }

    public void Actualizar(Order pedido)
    {
        contexto.Orders.Update(pedido);
    }

    public void Eliminar(Order pedido)
    {
        contexto.Orders.Remove(pedido);
    }

    public async Task<List<OrderResponseDto>> PedidosDespuesDeMayo()
    {
        var fecha = new DateTime(2025, 5, 1);
        var pedidos = await contexto.Orders
            .Where(pedido => pedido.Orderdate > fecha)
            .Select(pedido => new OrderResponseDto
            {
                Orderid = pedido.Orderid, Clientid = pedido.Clientid, Orderdate = pedido.Orderdate
            }).ToListAsync();
        return pedidos;
    }

    public async Task<List<OrderResponseDto>> PedidosConDetalles()
    {
        var pedidos = await contexto.Orders
            .OrderBy(pedido => pedido.Orderid)
            .Select(pedido => new OrderResponseDto
            {
                Orderid = pedido.Orderid, Clientid = pedido.Clientid, Orderdate = pedido.Orderdate,
                Details = pedido.Orderdetails.Select(detalle => new OrderProductDto
                {
                    Productid = detalle.Productid, ProductName = detalle.Product.Name, Quantity = detalle.Quantity
                }).ToList()
            }).ToListAsync();
        return pedidos;
    }

    public async Task<bool> TieneDetalles(int id)
    {
        return await contexto.Orderdetails.AnyAsync(detalle => detalle.Orderid == id);
    }
    
    public async Task<List<OrderDetailsDto>> PedidosConProductos()
    {
        var pedidos = await contexto.Orders
            .Include(pedido => pedido.Orderdetails)
            .ThenInclude(detalle => detalle.Product)
            .AsNoTracking()
            .Select(pedido => new OrderDetailsDto
            {
                OrderId = pedido.Orderid,
                OrderDate = pedido.Orderdate,
                Products = pedido.Orderdetails
                    .Select(detalle => new ProductDto
                    {
                        ProductName = detalle.Product.Name,
                        Quantity = detalle.Quantity,
                        Price = detalle.Product.Price
                    }).ToList()
            })
            .ToListAsync();

        return pedidos;
    }
    
    public async Task<List<SalesByClientDto>> VentasPorCliente()
    {
        var ventas = await contexto.Orders
            .Include(pedido => pedido.Orderdetails)
            .ThenInclude(detalle => detalle.Product)
            .AsNoTracking()
            .GroupBy(pedido => pedido.Clientid)
            .Select(grupo => new SalesByClientDto
            {
                ClientName = contexto.Clients
                    .Where(cliente => cliente.Clientid == grupo.Key)
                    .Select(cliente => cliente.Name)
                    .FirstOrDefault() ?? "",

                TotalSales = grupo.Sum(pedido => pedido.Orderdetails
                    .Sum(detalle =>
                        detalle.Quantity * detalle.Product.Price))
            })
            .OrderByDescending(cliente => cliente.TotalSales)
            .ToListAsync();

        return ventas;
    }
}
