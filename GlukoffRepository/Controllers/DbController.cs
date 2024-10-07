using System.Text.Json;
using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;
using GlukoffRepository.Services;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;


namespace GlukoffRepository.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class DbController : ControllerBase
{
    private readonly IServiceLocalDB _repository;

    public DbController(IServiceLocalDB repository)
    {
        _repository = repository;
    }

    [HttpGet("GetLocalOrders")]
    public async Task<ActionResult> GetLocalOrders()
    {
        var orders = await _repository.GetOrdersAsync();
        var json = JsonSerializer.SerializeToDocument(orders);
        return Ok(json);
    }

    [HttpGet("GetLocalOrder")]
    public async Task<ActionResult> GetLocalOrder(int orderId)
    {
        var order = await _repository.GetOrderAsync(orderId);
        var json = JsonSerializer.SerializeToDocument(order);
        return Ok(json);
        
    }

    [HttpPost("PostLocalOrder")]
    public async Task<ActionResult> PostLocalOrder(LocalOrder order)
    {
        await _repository.CreateOrderAsync(order);
        return Ok(order);
    }
}