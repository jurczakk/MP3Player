using System;

namespace MP3Player.Interfaces.Commands
{
    public interface IPlayBackCommand
    {
        event EventHandler CanExecuteChanged;
        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}