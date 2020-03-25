using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Helpers;
using System;
using System.Windows.Input;

namespace MP3Player.Commands
{
    public class ClearPlaylistCommand : ICommand, IClearPlaylistCommand
    {
        private readonly IPlaylistHelpers PlaylistHelpers;

        public ClearPlaylistCommand(IPlaylistHelpers playlistHelpers)
        {
            PlaylistHelpers = playlistHelpers;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return PlaylistHelpers.CanDeleteOrClear();
        }

        public void Execute(object parameter)
        {
            PlaylistHelpers.ClearPlaylist();
        }
    }
}
