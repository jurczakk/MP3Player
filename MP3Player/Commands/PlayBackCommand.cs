using MP3Player.Enums;
using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Models;
using System;
using System.Windows.Input;
using static MP3Player.Extensions.SongExtension;

namespace MP3Player.Commands
{
    public class PlayBackCommand : ICommand, IPlayBackCommand
    {
        private readonly ISong Song;

        public PlayBackCommand(ISong song)
        {
            Song = song;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return Song.CanPlay(parameter as IPlaylist);
        }

        public void Execute(object parameter)
        {
            Song.UniversalPlay(parameter as IPlaylist, PlayType.Back, new NAudio.Wave.WaveOut());
        }
    }
}
