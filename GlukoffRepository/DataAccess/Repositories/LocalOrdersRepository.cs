using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;

namespace GlukoffRepository.Services;

public class LocalOrdersRepository : SqliteRepository<LocalOrder>
{
    public LocalOrdersRepository(string connection) : base(connection)
    {
        
    }
}