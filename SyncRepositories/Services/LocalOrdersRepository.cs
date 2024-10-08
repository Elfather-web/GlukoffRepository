using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;
using Microsoft.Extensions.Configuration;

namespace SyncRepositories.Services;

public class LocalOrdersRepository : SqliteRepository<LocalOrder>, IServiceLocalDb
{
    public LocalOrdersRepository(string connection) : base(connection)
    {
    }

    public Task<List<LocalOrder>> GetOrdersAsync()
    {
        return SelectAsyncRows(CancellationToken.None);
    }

    public Task<LocalOrder> GetOrderAsync(int orderId)
    {
        return SelectAsync(orderId, CancellationToken.None);
    }


    public Task CreateOrderAsync(LocalOrder order)
    {
        return InsertAsync(order, CancellationToken.None);
    }

    public Task UpdateOrderAsync(LocalOrder order)
    {
        return UpdateAsync(order, CancellationToken.None);
    }

    public Task DeleteOrderAsync(LocalOrder order)
    {
        return DeleteAsync(order, CancellationToken.None);
    }
}