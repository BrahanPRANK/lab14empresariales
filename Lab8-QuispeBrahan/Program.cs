using Lab8_QuispeBrahan.Models;
using Lab8_QuispeBrahan.Repositories.Implements;
using Lab8_QuispeBrahan.Repositories.Interfaces;
using Lab8_QuispeBrahan.Services.Implements;
using Lab8_QuispeBrahan.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var constructor = WebApplication.CreateBuilder(args);

constructor.Services.AddControllers();
constructor.Services.AddEndpointsApiExplorer();
constructor.Services.AddSwaggerGen();
constructor.Services.AddDbContext<Laboratorio8Context>(opciones =>
    opciones.UseNpgsql(constructor.Configuration.GetConnectionString("DefaultConnection")));
constructor.Services.AddScoped<IClientRepository, ClientRepository>();
constructor.Services.AddScoped<IProductRepository, ProductRepository>();
constructor.Services.AddScoped<IOrderRepository, OrderRepository>();
constructor.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
constructor.Services.AddScoped<IUnitOfWork, UnitOfWork>();
constructor.Services.AddScoped<IClienteService, ClientService>();
constructor.Services.AddScoped<IProductService, ProductService>();
constructor.Services.AddScoped<IOrderService, OrderService>();
constructor.Services.AddScoped<IOrderDetailService, OrderDetailService>();

var aplicacion = constructor.Build();

using (var scope = aplicacion.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Laboratorio8Context>();
    db.Database.EnsureCreated();
}

if (aplicacion.Environment.IsDevelopment())
{
    aplicacion.UseSwagger();
    aplicacion.UseSwaggerUI();
}

aplicacion.UseHttpsRedirection();
aplicacion.UseAuthorization();
aplicacion.MapControllers();
aplicacion.Run();
