using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;

namespace Lab8_QuispeBrahan.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> Listar();
    Task<Order?> Buscar(int id);
    Task Agregar(Order pedido);
    void Actualizar(Order pedido);
    void Eliminar(Order pedido);

    Task<List<OrderResponseDto>> PedidosDespuesDeMayo();
    Task<List<OrderResponseDto>> PedidosConDetalles();
    Task<bool> TieneDetalles(int id);
    Task<List<OrderDetailsDto>> PedidosConProductos();
    Task<List<SalesByClientDto>> VentasPorCliente();
}
