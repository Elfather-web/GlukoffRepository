using GlukoffRepository.DataAccess;

namespace GlukoffRepository.Abstraction;

public interface IServiceLocalDb
{
    public Task<List<LocalOrder>> GetOrdersAsync();

    public Task<LocalOrder> GetOrderAsync(int orderId);

    public Task CreateOrderAsync(LocalOrder order);

    public Task UpdateOrderAsync(LocalOrder order);

    public Task DeleteOrderAsync(LocalOrder order);
}