using DataAcces;
using GlukoffRepository.DataAccess;
using SyncRepositories.Services;

namespace SyncRepositories;

public class Dbsync
{
    private readonly string _remoteconnection;
    private readonly string _localconnection;
    private readonly GlukoffOrdersRepository _remote;
    private readonly LocalOrdersRepository _local;
    
    public Dbsync(string localconnection, string remoteconnection)
    {
        _localconnection = localconnection;
        _remoteconnection = remoteconnection;
        _remote = new GlukoffOrdersRepository(_remoteconnection);
        _local = new LocalOrdersRepository(_localconnection);

    }

    public async void DoAsync()
    {
        var localorders = await _local.GetOrdersAsync();
        var remoteorders = await _remote.GetOrdersAsync();
        var localBuff = localorders.Select(l => BuffOrder.FromLocal(l)).ToHashSet();
        var remoteBuff = remoteorders.Select(r => BuffOrder.FromRemote(r)).ToHashSet();
        var resultbuff = localBuff.Except(remoteBuff);
        
        var remoteidorders = remoteBuff.Select(r => r.Id).ToHashSet();
        //Для каждого в резалтбафф проверяем есть ли он на ремуте по айди и не равен "Выдан"
        var j = 0;
        var s = 0;
        var d = 0;
        foreach (var result in resultbuff)
        {
            if (remoteidorders.Contains(result.Id) & result.Status != "Выдан")
            {
                //если есть и не равен "Выдан" обновляем 
                var updateorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status,
                    result.DateOrder, result.Barcode);
                await _remote.UpdateOrderAsync(updateorder);

            }
            else //если нет заливаем 
            if (result.Status != "Выдан")
            {
                var insertorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status, result.DateOrder, result.Barcode);
                await _remote.CreateOrderAsync(insertorder);
                
            }else 
            if (remoteidorders.Contains(result.Id) & result.Status == "Выдан")
            {
                var deleteorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status,
                    result.DateOrder, result.Barcode);
                await _remote.DeleteOrderAsync(deleteorder);
            }

            

        }
    }
}