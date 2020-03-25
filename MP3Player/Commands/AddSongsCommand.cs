using MP3Player.Interfaces.Commands;
using System;
using System.Windows.Input;
using MP3Player.Interfaces.Helpers;

namespace MP3Player.Commands
{
    public class AddSongsCommand : ICommand, IAddSongsCommand
    {
        private readonly IPlaylistHelpers PlaylistHelpers;

        public AddSongsCommand(IPlaylistHelpers playlistHelpers)
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
            PlaylistHelpers.OpenFileDialog();
        }
    }
}
