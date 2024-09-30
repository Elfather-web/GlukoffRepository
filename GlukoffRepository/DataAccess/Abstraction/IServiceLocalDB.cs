using GlukoffRepository.DataAccess;

namespace GlukoffRepository.Abstraction;

public interface IServiceLocalDB
{
    public Task<List<LocalOrder>> GetOrderAsync();
   
   

}