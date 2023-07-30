using Microsoft.UI.Xaml.Controls;
using SonyHeadphonesClient.API;
using SonyHeadphonesClient.Control;
using System;
using System.Threading;
using System.Windows.Input;

namespace SonyHeadphonesClient.UICommand
{
    public class LeftClickCommand : ICommand
    {
        private Action executeAction;

        private bool _CanExecute = true;
        public LeftClickCommand()
        {
            executeAction = Show;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            Console.WriteLine(_CanExecute);
            Console.WriteLine(MainWindow.IsShow);
            if (_CanExecute)
                return !MainWindow.IsShow;
            else
                return false;
        }
        public void Execute(object parameter)
        {
            _CanExecute = false;
            executeAction();
            _CanExecute = true;
        }

        private void Show()
        {
            Thread childThread = new Thread(MainWindow.ShowWindow);
            childThread.Start();
            WinAPI.SetForegroundWindow(MainWindow.hwnd);
        }
    }
    public class ExitCommand : ICommand
    {
        private Action executeAction;
        public ExitCommand()
        {
            executeAction = () => Environment.Exit(0);
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter)
        {
            executeAction();
        }
    }

    public class AutoRuntCommand : ICommand
    {
        private Action executeAction;

        public delegate void CheckAutoStart();
        public AutoRuntCommand(CheckAutoStart check)
        {
            executeAction = () =>
            {
                AutoRun.SetMeStart(!AutoRun.IsAutoStart()); 
                check();
            };
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter)
        {
            executeAction();
        }
    }
}
