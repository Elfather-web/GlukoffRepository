namespace GlukoffRepository.Abstraction;



public interface IRepository<TEntity>
{
    
    Task <TEntity> SelectAsync(int id,  CancellationToken token);
   
    Task<List<TEntity>> SelectAsyncRows(CancellationToken token);
   
    Task InsertAsync(TEntity entity, CancellationToken token);
    
    Task UpdateAsync(TEntity entity, CancellationToken token);
    
    Task DeleteAsync(TEntity entity, CancellationToken token);

}
