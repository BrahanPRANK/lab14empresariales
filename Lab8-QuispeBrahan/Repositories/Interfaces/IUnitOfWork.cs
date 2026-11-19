namespace Lab8_QuispeBrahan.Repositories.Interfaces;

public interface IUnitOfWork
{
    IClientRepository Clientes { get; }
    IProductRepository Productos { get; }
    IOrderRepository Pedidos { get; }
    IOrderDetailRepository Detalles { get; }
    Task<int> GuardarCambios();
}
