using System.IO;
using System.Windows;
using GlukoffRepository.DataAccess;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SyncRepositories.Services;

namespace SyncRepositories;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Settings _settings = new Settings();

    public MainWindow()
    {
        
        var json = File.ReadAllText(".\\appsettings.json");
        if (!string.IsNullOrEmpty(json))
        {
            _settings = JsonConvert.DeserializeObject<Settings>(json);
        }

        InitializeComponent();
    }

    private void Upload_OnClick(object sender, RoutedEventArgs e)
    {
        var connection = _settings.ConnectionStrings.LocalOrderConnection;
        LocalOrdersRepository Localorders = new LocalOrdersRepository(connection);
        var orders = Localorders.GetOrdersAsync();
        //скачиваем все строки с локальной и удаленной дб
        //исключаем неравные строки
        //заливаем отсутсвующие на удаеленный
        //обновляем локальный
        //"LINQ" 
    }
}