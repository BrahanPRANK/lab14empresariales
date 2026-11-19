using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8_QuispeBrahan.Repositories.Implements;

public class ClientRepository : IClientRepository
{
    private readonly Laboratorio8Context contexto;

    public ClientRepository(Laboratorio8Context contexto)
    {
        this.contexto = contexto;
    }

    public async Task<List<Client>> Listar()
    {
        return await contexto.Clients.ToListAsync();
    }

    public async Task<Client?> Buscar(int id)
    {
        return await contexto.Clients.FindAsync(id);
    }

    public async Task Agregar(Client cliente)
    {
        await contexto.Clients.AddAsync(cliente);
    }

    public void Actualizar(Client cliente)
    {
        contexto.Clients.Update(cliente);
    }

    public void Eliminar(Client cliente)
    {
        contexto.Clients.Remove(cliente);
    }

    public async Task<List<ClientResponseDto>> ClientesConJuan()
    {
        var clientesJuan = await contexto.Clients.Where(cliente => cliente.Name.Contains("Juan")).Select(cliente => new ClientResponseDto
            {
                Clientid = cliente.Clientid, Name = cliente.Name, Email = cliente.Email
            }).ToListAsync();
        return clientesJuan;
    }

    public async Task<ClientOrderCountDto?> ClienteConMasPedidos()
    {
        var cliente = await contexto.Orders
            .GroupBy(pedido => new { pedido.Clientid, pedido.Client.Name })
            .Select(grupo => new ClientOrderCountDto
            {
                Clientid = grupo.Key.Clientid, ClientName = grupo.Key.Name, OrderCount = grupo.Count()
            })
            .OrderByDescending(cliente => cliente.OrderCount)
            .ThenBy(cliente => cliente.Clientid)
            .FirstOrDefaultAsync();
        return cliente;
    }

    public async Task<List<ClientResponseDto>> ClientesDelProducto2()
    {
        var clientes = await contexto.Orderdetails
            .Where(detalle => detalle.Productid == 2)
            .Select(detalle => new ClientResponseDto
            {
                Clientid = detalle.Order.Clientid,
                Name = detalle.Order.Client.Name,
                Email = detalle.Order.Client.Email
            }).Distinct().ToListAsync();
        return clientes;
    }

    public async Task<bool> TienePedidos(int id)
    {
        return await contexto.Orders.AnyAsync(pedido => pedido.Clientid == id);
    }
    
    public async Task<List<ClientOrderDto?>> ConsultaNueva()
    {
        var clientes = await contexto.Clients
            .AsNoTracking()
            .Select(cliente => new ClientOrderDto
            {
                ClientName = cliente.Name,
                Orders = cliente.Orders
                    .Select(pedido => new OrderDto
                    {
                        OrderId = pedido.Orderid,
                        OrderDate = pedido.Orderdate
                    }).ToList()
            })
            .ToListAsync();

        return clientes;
    }

    public async Task<object> ClientesConTotalProductos()
    {
        var clientes = await contexto.Clients
            .AsNoTracking()
            .Select(cliente => new
            {
                ClientName = cliente.Name,
                TotalProducts = cliente.Orders
                    .SelectMany(pedido => pedido.Orderdetails)
                    .Sum(detalle => (long?)detalle.Quantity) ?? 0
            })
            .ToListAsync();

        return clientes;
    }
}
