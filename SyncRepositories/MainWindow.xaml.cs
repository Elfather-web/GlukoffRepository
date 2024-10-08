using System.IO;
using System.Windows;
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
        var json = File.ReadAllText("appsetings.json");
        if (!string.IsNullOrEmpty(json))
        {
            _settings = JsonConvert.DeserializeObject<Settings>(json);
        }

        InitializeComponent();
    }

    private void Upload_OnClick(object sender, RoutedEventArgs e)
    {

        throw new Exception();
    }
}