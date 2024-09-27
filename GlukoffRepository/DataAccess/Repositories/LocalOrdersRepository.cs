using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;


namespace GlukoffRepository.Services;

public class LocalOrdersRepository : SqliteRepository<LocalOrder>, IServiceLocalDB
{
    public LocalOrdersRepository(string connection="Data Source=/Users/elena/Desktop/baza.sqlite") : base(connection)
    {
    }

    public Task<IEnumerable<LocalOrder>> GetOrderAsync()
    {
        return SelectAsync(CancellationToken.None);
    }
}