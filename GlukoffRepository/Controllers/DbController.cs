using System.Text.Json;
using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;
using GlukoffRepository.Services;
using Microsoft.AspNetCore.Mvc;


namespace GlukoffRepository.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class DbController : ControllerBase
{
    private  readonly IServiceLocalDB _repository;

    public DbController(IServiceLocalDB repository)
    {
        _repository = repository;
    }

    [HttpGet("GetLocalOrders")]
    public async Task<ActionResult> GetLocalOrders()
    {
        // _repository = new LocalOrdersRepository("Data Source=/Users/elena/Desktop/baza.sqlite");
        var orders = await _repository.GetOrderAsync();
        var order = orders.FirstOrDefault();
        var json = JsonSerializer.SerializeToDocument(order);
        return Ok(json);
    }


}