using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;
namespace GlukoffRepository.Services;

public class GlukoffOrdersRepository : MySqlRepository<RemoteOrder>, IServiceMsqlDB
{
    public GlukoffOrdersRepository(string connection = "server=localhost; user id=root; password=password; database=mysql;") : base(connection)
    {
        
    }
    public Task<List<RemoteOrder>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RemoteOrder> GetOrderAsync(int orderId)
    {
        return SelectAsync(orderId, CancellationToken.None);
    }

    public Task CreateOrderAsync(RemoteOrder order)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderAsync(RemoteOrder order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOrderAsync(RemoteOrder order)
    {
        throw new NotImplementedException();
    }
}