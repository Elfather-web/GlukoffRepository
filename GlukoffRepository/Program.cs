
using System.Text.Json;
using GlukoffRepository.Abstraction;
using GlukoffRepository.Controllers;
using GlukoffRepository.DataAccess;
using GlukoffRepository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IServiceLocalDB, LocalOrdersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();

// namespace GlukoffRepository;

// public class Program
// {
//     public static async Task Main(string[] args)
//     {
//         
//         var repository = new LocalOrdersRepository("Data Source=/Users/elena/Desktop/baza.sqlite");
//         var orders = await repository.SelectAsync(CancellationToken.None);
//         var order = orders.FirstOrDefault();
//         var json = JsonSerializer.Serialize(orders.FirstOrDefault());
//         json = JsonSerializer.Serialize(order);
//         Console.WriteLine(json);
//     }
//     
// }

