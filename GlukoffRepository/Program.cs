// using GlukoffRepository.Abstraction;
// using GlukoffRepository.Services;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
//
//
//
// app.Run();
//

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using GlukoffRepository.Services;

namespace GlukoffRepository;

public class Program
{
    public static async Task Main(string[] args)
    {
        var repository = new LocalOrdersRepository("Data Source= C:\\Users\\Alf\\Downloads\\MyWork\\MyWork2\\bin\\Debug\\baza.sqlite");
        var orders = await repository.SelectAsync(CancellationToken.None);
        var order = orders.FirstOrDefault();
        var json = JsonSerializer.Serialize(orders.FirstOrDefault());
        Console.WriteLine(json);
    }
}