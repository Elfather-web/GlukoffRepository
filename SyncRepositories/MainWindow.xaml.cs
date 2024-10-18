﻿using System.IO;
using System.Windows;
using System.Windows.Documents;
using DataAcces;
using GlukoffRepository.DataAccess;
using Newtonsoft.Json;
using Org.BouncyCastle.Math;
using SyncRepositories.Services;
using BigInteger = System.Numerics.BigInteger;
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
        
        var jsonenc = File.ReadAllText(".\\appsettings.json");
        var json = _settings.GetConnection(jsonenc);
        
        if (!string.IsNullOrEmpty(json)) _settings = JsonConvert.DeserializeObject<Settings>(json)!;
        
        
       
        InitializeComponent();
    }

    private async void Upload_OnClick(object sender, RoutedEventArgs e)
    {
        Dbsync worker = new Dbsync(_settings.ConnectionStrings.LocalOrderConnection,
            _settings.ConnectionStrings.RemoteOrderConnection);
        worker.DoAsync();




        // var localConnection = _settings.ConnectionStrings.LocalOrderConnection;
        // var remoteConnection = _settings.ConnectionStrings.RemoteOrderConnection;
        // var remote = new GlukoffOrdersRepository(remoteConnection);
        // var local = new LocalOrdersRepository(localConnection);
        //
        // var localorders = await local.GetOrdersAsync();
        // var remoteorders = await remote.GetOrdersAsync();
        //
        // var localBuff = localorders.Select(l => BuffOrder.FromLocal(l)).ToHashSet();
        // var remoteBuff = remoteorders.Select(r => BuffOrder.FromRemote(r)).ToHashSet();
        //
        // var resultbuff = localBuff.Except(remoteBuff);
        //
        // var remoteidorders = remoteBuff.Select(r => r.Id).ToHashSet();
        // var status = remoteBuff.Select(s => s.Status).ToHashSet();
        //
        // //Для каждого в резалтбафф проверяем есть ли он на ремуте по айди и не равен "Выдан"
        // foreach (var result in resultbuff)
        // {
        //     if (remoteidorders.Contains(result.Id) & result.Status != "Выдан")
        //     {
        //         //если есть и не равен "Выдан" обновляем 
        //         var updateorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status,
        //             result.DateOrder, result.Barcode);
        //         await remote.UpdateOrderAsync(updateorder);
        //
        //     }
        //     else //если нет заливаем 
        //     if (result.Status != "Выдан")
        //     {
        //         var insertorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status, result.DateOrder, result.Barcode);
        //         await remote.CreateOrderAsync(insertorder);
        //         
        //     }else 
        //     if (remoteidorders.Contains(result.Id) & result.Status == "Выдан")
        //     {
        //         var deleteorder = new RemoteOrder(Convert.ToInt32(result.Id), result.Tittle, result.Status,
        //             result.DateOrder, result.Barcode);
        //         await remote.DeleteOrderAsync(deleteorder);
        //     }



    }
        
    }
    //1) получить доступ к базам данных, скопировать данные в свою. https://stackoverflow.com/questions/22381577/mysql-workbench-how-to-export-mysql-database-to-sql-file
    //2) сделать отсечку, не загружать выданные 
    //3) пашины корриктировки по базе данных(по заказам)
    //4) протестировать
    //5) еще раз сделать дамп перед продакшеном
    //6) хранить в ремоуте по бар коду вместо айди
    //7) найти баркод, вывести на печать в инт
   //8) музычка на фоне:))
    //поменять в серверной абстракции ordertittle на ordertitle, записывать на сервер в ordertitle 