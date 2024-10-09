using GlukoffRepository.DataAccess;

namespace DataAcces;

public record BuffOrder(long Id, string Tittle, string Status, DateTime DateOrder)
{
    public static BuffOrder? FromLocal(LocalOrder localOrder) => DateTime.TryParse(localOrder.DateOrder, out var dateTime)
            ? new BuffOrder(localOrder.Id, localOrder.Tittle, localOrder.Status, dateTime)
            : null;

    public static BuffOrder FromRemote(RemoteOrder remoteOrder) => 
        new BuffOrder(remoteOrder.Id, remoteOrder.Tittle, remoteOrder.Status, remoteOrder.DateOrder);
    
}