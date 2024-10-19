using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification.Interop;

namespace SyncRepositories;

public class NotifyIconViewModel
{
    // private MainWindow? GetMainWindow()
    // {
    //     return Application.Current.MainWindow as MainWindow;
    //     
    // }

    // public ICommand ShowWindowCommand
    // {
    //     get
    //     {
    //         return new DelegateCommand
    //         {
    //             CanExecuteFunc = () => this.GetMainWindow() == null,
    //             CommandAction = () =>
    //             {
    //                 Application.Current.MainWindow = new MainWindow();
    //                 Application.Current.MainWindow.Show();
    //             }
    //         };
    //     }
    // }
    
    
    public ICommand ExitApplicationCommand
    {
        get { return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() }; }
    }
}