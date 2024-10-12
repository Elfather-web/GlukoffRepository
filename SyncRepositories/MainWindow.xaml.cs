using System.IO;
using System.Windows;
using System.Windows.Documents;
using DataAcces;
using GlukoffRepository.DataAccess;
using Newtonsoft.Json;
using SyncRepositories.Services;
using Order = Mysqlx.Crud.Order;

namespace SyncRepositories;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Settings _settings = new();

    public MainWindow()
    {
        var json = File.ReadAllText(".\\appsettings.json");
        if (!string.IsNullOrEmpty(json)) _settings = JsonConvert.DeserializeObject<Settings>(json);

        InitializeComponent();
    }

    private async void Upload_OnClick(object sender, RoutedEventArgs e)
    {
        var localConnection = _settings.ConnectionStrings.LocalOrderConnection;
        var remoteConnection = _settings.ConnectionStrings.RemoteOrderConnection;
        var remote = new GlukoffOrdersRepository(remoteConnection);
        var local = new LocalOrdersRepository(localConnection);

        var localorders = await local.GetOrdersAsync();
        var remoteorders = await remote.GetOrdersAsync();

        var localBuff = localorders.Select(l => BuffOrder.FromLocal(l)).ToHashSet();
        var remoteBuff = remoteorders.Select(r => BuffOrder.FromRemote(r)).ToHashSet();
       
        var resultbuff = localBuff.Except(remoteBuff);
        
       
        var remoteidorders = remoteBuff.Select(r => r.Id).ToHashSet();
        
        //Для каждого в резалтбафф проверяем есть ли он на ремуте по айди
        foreach (var result in resultbuff)
        {
            if (remoteidorders.Contains(result.Id))
            {
                //если есть обновляем
                var updateorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status,
                    result.DateOrder);
                await remote.UpdateOrderAsync(updateorder);

            }
            else
            {
                //если нет заливаем
                var insertorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status, result.DateOrder);
                await remote.CreateOrderAsync(insertorder);
            }

            await Task.Delay(500); //убрать после тестов

        }
        
    }
    //1) получить доступ к базам данных, скопировать данные в свою. https://stackoverflow.com/questions/22381577/mysql-workbench-how-to-export-mysql-database-to-sql-file
    //2) сделать отсечку
    //3) пашины корриктировки по базе данных(по заказам)
    //4) протестировать
    //5) еще раз сделать дамп перед продакшеном
    
}