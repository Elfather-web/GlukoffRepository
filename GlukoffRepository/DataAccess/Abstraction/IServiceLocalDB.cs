using GlukoffRepository.DataAccess;

namespace GlukoffRepository.Abstraction;

public interface IServiceLocalDB
{
    public Task<IEnumerable<LocalOrder>> GetOrderAsync();
   
   

}