using GlukoffRepository.DataAccess;

namespace GlukoffRepository.Abstraction;

public interface IServiceMsqlDb
{
    public Task<List<RemoteOrder>> GetOrdersAsync();

    public Task<RemoteOrder> GetOrderAsync(int orderId);

    public Task CreateOrderAsync(RemoteOrder order);

    public Task UpdateOrderAsync(RemoteOrder order);

    public Task DeleteOrderAsync(RemoteOrder order);
}