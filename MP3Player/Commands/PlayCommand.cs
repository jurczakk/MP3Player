using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Models;
using System;
using System.Windows.Input;
using static MP3Player.Extensions.SongExtension;

namespace MP3Player.Commands
{
    public class PlayCommand : ICommand, IPlayCommand
    {
        private readonly ISong Song;

        public PlayCommand(ISong song)
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
            Song.Play(parameter as IPlaylist, new NAudio.Wave.WaveOut());
        }
    }
}
