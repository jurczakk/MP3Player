using System;

namespace MP3Player.Interfaces
{
    public interface IClearPlaylistCommand
    {
        event EventHandler CanExecuteChanged;

        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}