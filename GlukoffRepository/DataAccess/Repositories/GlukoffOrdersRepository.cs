﻿using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;
namespace GlukoffRepository.Services;

public class GlukoffOrdersRepository : MySqlRepository<RemoteOrder>, IServiceMsqlDb
{
    public GlukoffOrdersRepository(string connection = "server=localhost; user id=root; password=password; database=mysql;") : base(connection)
    {
        
    }
    public Task<List<RemoteOrder>> GetOrdersAsync()
    {
        return SelectAsyncRows(CancellationToken.None);
    }

    public Task<RemoteOrder> GetOrderAsync(int orderId)
    {
        return SelectAsync(orderId, CancellationToken.None);
    }

    public Task CreateOrderAsync(RemoteOrder order)
    {
        return InsertAsync(order, CancellationToken.None);
    }

    public Task UpdateOrderAsync(RemoteOrder order)
    {
        return UpdateAsync(order, CancellationToken.None);
    }

    public Task DeleteOrderAsync(RemoteOrder order)
    {
        return DeleteAsync(order, CancellationToken.None);
    }
}