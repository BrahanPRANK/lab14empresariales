using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Repositories.Interfaces;

namespace Lab8_QuispeBrahan.Repositories.Implements;

public class UnitOfWork : IUnitOfWork
{
    private readonly Laboratorio8Context contexto;

    public IClientRepository Clientes { get; }
    public IProductRepository Productos { get; }
    public IOrderRepository Pedidos { get; }
    public IOrderDetailRepository Detalles { get; }

    public UnitOfWork(Laboratorio8Context contexto, IClientRepository clientes,
        IProductRepository productos, IOrderRepository pedidos, IOrderDetailRepository detalles)
    {
        this.contexto = contexto;
        Clientes = clientes;
        Productos = productos;
        Pedidos = pedidos;
        Detalles = detalles;
    }

    public async Task<int> GuardarCambios()
    {
        return await contexto.SaveChangesAsync();
    }
}
