using MP3Player.Interfaces.Models;
using MP3Player.Interfaces.Commands;
using System;
using System.Windows.Input;
using static MP3Player.Extensions.PlaylistExtension;

namespace MP3Player.Commands
{
    public class AddSongsCommand : ICommand, IAddSongsCommand
    {
        private readonly IPlaylist Playlist;

        public AddSongsCommand(IPlaylist playlist)
        {
            Playlist = playlist;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return Playlist.CanDeleteOrClear();
        }

        public void Execute(object parameter)
        {
            Playlist.OpenFileDialog();
        }
    }
}
