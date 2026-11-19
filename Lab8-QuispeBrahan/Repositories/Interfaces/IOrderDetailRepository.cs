using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;

namespace Lab8_QuispeBrahan.Repositories.Interfaces;

public interface IOrderDetailRepository
{
    Task<List<Orderdetail>> Listar();
    Task<Orderdetail?> Buscar(int id);
    Task Agregar(Orderdetail detalle);
    void Actualizar(Orderdetail detalle);
    void Eliminar(Orderdetail detalle);

    Task<List<OrderProductDto>> ProductosDelPedido1();
    Task<long> CantidadDelPedido1();
}
