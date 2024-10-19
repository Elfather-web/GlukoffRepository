using System.Collections;
using System.Drawing;
using DataAcces;
using GlukoffRepository.DataAccess;
using SyncRepositories.Services;
using Forms = System.Windows.Forms;


namespace SyncRepositories;

public class Dbsync
{
    private readonly string _remoteconnection;
    private readonly string _localconnection;
    private readonly GlukoffOrdersRepository _remote;
    private readonly LocalOrdersRepository _local;
    private Forms.NotifyIcon _notifyIcon;
    public Messege _messege;


    public Dbsync(string localconnection, string remoteconnection)
    {
        _localconnection = localconnection;
        _remoteconnection = remoteconnection;
        _remote = new GlukoffOrdersRepository(_remoteconnection);
        _local = new LocalOrdersRepository(_localconnection);
        _notifyIcon = new Forms.NotifyIcon();
        _messege = new Messege();

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
        var i = 0;
        var j = 0;
        var l = 0;
        foreach (var result in resultbuff)
        {
            if (remoteidorders.Contains(result.Id))
            {
                
                if (result.Status == "Выдан")
                {
                    var deleteorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status,
                        result.DateOrder, result.Barcode);
                    await _remote.DeleteOrderAsync(deleteorder);
                    l++;
                
                }
                else
                {
                    if (!Compare(remoteorders, result))
                    {
                        //если есть и не равен "Выдан" обновляем 
                        var updateorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status,
                            result.DateOrder, result.Barcode);
                        await _remote.UpdateOrderAsync(updateorder);
                        i++;
                    }
                    
                }

            }
            else //если нет заливаем 
            if (result.Status != "Выдан")
            {
                var insertorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status, result.DateOrder, result.Barcode);
                await _remote.CreateOrderAsync(insertorder);
                j++;
                
            }else 
            
            _messege.Updated = i;
            _messege.Uplouded = j;
            _messege.Deleted = l;



        }
        _notifyIcon.Text = "Загружено: " + _messege.Uplouded.ToString() + "\n"+ 
                           "Удалено: " + _messege.Deleted.ToString()+ "\n" + 
                           "Обновлено: " + _messege.Updated.ToString();
        _notifyIcon.Visible = true;
        
        _notifyIcon.Icon = SystemIcons.Application;
        _notifyIcon.ShowBalloonTip(3000, "Заявки" , _notifyIcon.Text, _notifyIcon.BalloonTipIcon);
        
    }

    private static bool Compare(List<RemoteOrder> remoteorders, BuffOrder result)
    {
        var order= remoteorders.FirstOrDefault(f => f.Id == result.Id);
        if (order == null) return false;
        if (order.DateOrder != result.DateOrder) return false;
        if (order.Status != result.Status) return false;
        if (order.Barcode != result.Barcode) return false;
        if (order.Title != result.Tittle) return false;
        return true;
    }
}