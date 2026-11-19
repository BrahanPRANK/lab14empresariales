using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Models.DTOs;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8_QuispeBrahan.Services.Implements;

public class ClientService : IClienteService
{
    private readonly IUnitOfWork unidad;

    public ClientService(IUnitOfWork unidad)
    {
        this.unidad = unidad;
    }

    public async Task<IActionResult> Listar()
    {
        try
        {
            var registros = await unidad.Clientes.Listar();
            var lista = new List<ClientResponseDto>();
            foreach (var cliente in registros)
            {
                lista.Add(Convertir(cliente));
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
            var cliente = await unidad.Clientes.Buscar(id);
            if (cliente == null)
                return new NotFoundResult();
            return new OkObjectResult(Convertir(cliente));
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Agregar(ClientCreateDto datos)
    {
        try
        {
            var cliente = new Client { Name = datos.Name, Email = datos.Email };
            await unidad.Clientes.Agregar(cliente);
            await unidad.GuardarCambios();
            return new CreatedResult($"/api/clients/{cliente.Clientid}", Convertir(cliente));
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> Actualizar(int id, ClientCreateDto datos)
    {
        try
        {
            var cliente = await unidad.Clientes.Buscar(id);
            if (cliente == null)
                return new NotFoundResult();
            cliente.Name = datos.Name;
            cliente.Email = datos.Email;
            unidad.Clientes.Actualizar(cliente);
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
            var cliente = await unidad.Clientes.Buscar(id);
            if (cliente == null)
                return new NotFoundResult();
            if (await unidad.Clientes.TienePedidos(id))
                return new ConflictObjectResult(new { mensaje = "No se puede eliminar: tiene pedidos asociados." });
            unidad.Clientes.Eliminar(cliente);
            await unidad.GuardarCambios();
            return new NoContentResult();
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> ClientesConJuan()
    {
        try
        {
            var resultado = await unidad.Clientes.ClientesConJuan();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> ClienteConMasPedidos()
    {
        try
        {
            var resultado = await unidad.Clientes.ClienteConMasPedidos();
            if (resultado == null)
                return new NotFoundResult();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> ClientesDelProducto2()
    {
        try
        {
            var resultado = await unidad.Clientes.ClientesDelProducto2();
            return new OkObjectResult(resultado);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.") { StatusCode = 500 };
        }
    }

    private static ClientResponseDto Convertir(Client cliente)
    {
        return new ClientResponseDto
        {
            Clientid = cliente.Clientid,
            Name = cliente.Name,
            Email = cliente.Email
        };
    }

    public async Task<IActionResult> ClientesConTotalProductos()
    {
        try
        {
            var clientes = await unidad.Clientes.ClientesConTotalProductos();
            return new OkObjectResult(clientes);
        }
        catch (Exception)
        {
            return new ObjectResult("No se pudo completar la operación.")
            {
                StatusCode = 500
            };
        }
    }
    
    public async Task<IActionResult> ClientesConPedidos()
    {
        try
        {
            var clientes = await unidad.Clientes.ConsultaNueva();
            return new OkObjectResult(clientes);
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
