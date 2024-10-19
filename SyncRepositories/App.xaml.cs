using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using Forms = System.Windows.Forms;
using Hardcodet.Wpf.TaskbarNotification;
using Newtonsoft.Json;
using Application = System.Windows.Application;

namespace SyncRepositories;

public partial class App : Application
{
    
    private TaskbarIcon _taskbarIcon;
    private  readonly Settings _settings = new();
    

    public App()
    {
        var jsonenc = File.ReadAllText(".\\appsettings.json");
        var json = _settings.GetConnection(jsonenc);
        if (!string.IsNullOrEmpty(json)) _settings = JsonConvert.DeserializeObject<Settings>(json)!;
        MainWatcher();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        
        _taskbarIcon = (TaskbarIcon) FindResource("NotifyIcon");
        
        base.OnStartup(e);
        
        

    }
    
    
    public void MainWatcher()
    {
        string selfLocation = Assembly.GetEntryAssembly().Location;
        var selfDirectory = Path.GetDirectoryName(selfLocation);
        FileSystemWatcher watcher = new FileSystemWatcher($@"{selfDirectory}");
        watcher.Filter = "*sqlite";
        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.Size;
        watcher.EnableRaisingEvents = true;
        watcher.Changed += OnChanged;
        
    }
    private void OnChanged(object sender, FileSystemEventArgs e)
    {
       
        if (e.ChangeType == WatcherChangeTypes.Changed)
        {
            Dbsync worker = new Dbsync(_settings.ConnectionStrings.LocalOrderConnection,
                _settings.ConnectionStrings.RemoteOrderConnection);
            Messege messege = new Messege();
            worker.DoAsync();
           
        } 
      
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        _taskbarIcon.Dispose();
        base.OnExit(e);
    }
}