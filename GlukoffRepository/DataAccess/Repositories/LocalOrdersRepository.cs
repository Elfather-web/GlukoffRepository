using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;


namespace GlukoffRepository.Services;

public class LocalOrdersRepository : SqliteRepository<LocalOrder>, IServiceLocalDB
{
    private IServiceLocalDB _serviceLocalDbImplementation;

    public LocalOrdersRepository(string connection="Data Source=/Users/aleksandrlebedev/Documents/GitHub/GlukoffRepository/GlukoffRepository/baza.sqlite") : base(connection)
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

   
}