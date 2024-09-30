using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;


namespace GlukoffRepository.Services;

public class LocalOrdersRepository : SqliteRepository<LocalOrder>, IServiceLocalDB
{
    public LocalOrdersRepository(string connection="Data Source=/Users/aleksandrlebedev/Documents/GitHub/GlukoffRepository/GlukoffRepository/baza.sqlite") : base(connection)
    {
    }

    public Task<List<LocalOrder>> GetOrderAsync()
    {
        return SelectAsync(CancellationToken.None);
    }
}