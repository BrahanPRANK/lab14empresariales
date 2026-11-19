using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8_QuispeBrahan.Repositories.Implements;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly Laboratorio8Context contexto;

    public OrderDetailRepository(Laboratorio8Context contexto)
    {
        this.contexto = contexto;
    }

    public async Task<List<Orderdetail>> Listar()
    {
        return await contexto.Orderdetails.ToListAsync();
    }

    public async Task<Orderdetail?> Buscar(int id)
    {
        return await contexto.Orderdetails.FindAsync(id);
    }

    public async Task Agregar(Orderdetail detalle)
    {
        await contexto.Orderdetails.AddAsync(detalle);
    }

    public void Actualizar(Orderdetail detalle)
    {
        contexto.Orderdetails.Update(detalle);
    }

    public void Eliminar(Orderdetail detalle)
    {
        contexto.Orderdetails.Remove(detalle);
    }

    public async Task<List<OrderProductDto>> ProductosDelPedido1()
    {
        var productos = await contexto.Orderdetails
            .Where(detalle => detalle.Orderid == 1)
            .Select(detalle => new OrderProductDto
            {
                Productid = detalle.Productid, ProductName = detalle.Product.Name, Quantity = detalle.Quantity
            }).ToListAsync();
        return productos;
    }

    public async Task<long> CantidadDelPedido1()
    {
        var cantidad = await contexto.Orderdetails
            .Where(detalle => detalle.Orderid == 1)
            .Select(detalle => (long)detalle.Quantity)
            .SumAsync();
        return cantidad;
    }
}
