using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;

namespace Lab8_QuispeBrahan.Repositories.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> Listar();
    Task<Client?> Buscar(int id);
    Task Agregar(Client cliente);
    void Actualizar(Client cliente);
    void Eliminar(Client cliente);

    Task<List<ClientResponseDto>> ClientesConJuan();
    Task<ClientOrderCountDto?> ClienteConMasPedidos();
    Task<List<ClientResponseDto>> ClientesDelProducto2();
    Task<bool> TienePedidos(int id);
    Task<List<ClientOrderDto?>> ConsultaNueva();
    Task<object> ClientesConTotalProductos();
    
}
