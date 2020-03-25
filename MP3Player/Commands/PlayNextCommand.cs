using MP3Player.Enums;
using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Helpers;
using MP3Player.Interfaces.Models;
using System;
using System.Windows.Input;

namespace MP3Player.Commands
{
    public class PlayNextCommand : ICommand, IPlayNextCommand
    {
        private readonly ISongHelpers SongHelpers;

        public PlayNextCommand(ISongHelpers songHelpers)
        {
            SongHelpers = songHelpers;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return SongHelpers.CanPlay(parameter as IPlaylist);
        }

        public void Execute(object parameter)
        {
            SongHelpers.UniversalPlay(parameter as IPlaylist, PlayType.Next, new NAudio.Wave.WaveOut());
        }
    }
}
