using System.Text.Json;
using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;
using Microsoft.AspNetCore.Mvc;



namespace GlukoffRepository.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class LocalDbController : ControllerBase
{
    private readonly IServiceLocalDb _repository;

    public LocalDbController(IServiceLocalDb repository)
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
        var json = JsonSerializer.SerializeToDocument(await _repository.GetOrdersAsync());
        return Ok(json);
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
    private readonly IServiceMsqlDb _repository;

    public RemoteDbController(IServiceMsqlDb repository)
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

    [HttpGet("GetGlukOffOrders")]

    public async Task<ActionResult> GetRemoteOrders()
    {
        var orders = await _repository.GetOrdersAsync();
        var json = JsonSerializer.SerializeToDocument(orders);
        return Ok(json);
    }
    
    [HttpPost("PostGlukOffOrder")]
    public async Task<ActionResult> PostRemoteOrder(RemoteOrder order)
    {
        await _repository.CreateOrderAsync(order);
        return Ok(order);
    }

    [HttpPut("UpdateGlukOffOrder")]
    public async Task<ActionResult> UpdateRemoteOrder(RemoteOrder order)
    {
        await _repository.UpdateOrderAsync(order);
        var json = JsonSerializer.SerializeToDocument(await _repository.GetOrdersAsync());
        return Ok(json);
    }

    [HttpDelete("DeleteGlukOffOrder")]
    public async Task<ActionResult> DeleteRemoteOrder(RemoteOrder order)
    {
        await _repository.DeleteOrderAsync(order);
        return Ok(JsonSerializer.SerializeToDocument(await _repository.GetOrdersAsync()));
    }
}

