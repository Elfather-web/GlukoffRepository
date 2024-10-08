using System.Text.Json;
using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;
using GlukoffRepository.Services;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;


namespace GlukoffRepository.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class LocalDBController : ControllerBase
{
    private readonly IServiceLocalDB _repository;

    public LocalDBController(IServiceLocalDB repository)
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

    [HttpPut("UpdateLocalOrder")]
    public async Task<ActionResult> UpdateLocalOrder(LocalOrder order)
    {
        await _repository.UpdateOrderAsync(order);
        return Ok(JsonSerializer.SerializeToDocument(await _repository.GetOrdersAsync()));
    }

    [HttpDelete("DeleteLocalOrder")]

    public async Task<ActionResult> DeleteLocalOrder(LocalOrder order)
    {
        await _repository.DeleteOrderAsync(order);
        return Ok(JsonSerializer.SerializeToDocument(await _repository.GetOrdersAsync()));
    }
}

[ApiController]
[Route("api/[Controller]")]
public class RemoteDbController : ControllerBase
{
    private readonly IServiceMsqlDB _repository;

    public RemoteDbController(IServiceMsqlDB repository)
    {
        _repository = repository;
    }
        
    [HttpGet("GetGlukOffOrder")]
    public async Task<ActionResult> GetRemoteOrder(int orderId)
    {
        var order = await _repository.GetOrderAsync(orderId);
        var json = JsonSerializer.SerializeToDocument(order);
        return Ok(json);
        
    }
}

