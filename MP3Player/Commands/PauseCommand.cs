using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Models;
using System;
using System.Windows.Input;
using static MP3Player.Extensions.SongExtension;

namespace MP3Player.Commands
{
    public class PauseCommand : ICommand, IPauseCommand
    {
        private readonly ISong Song;

        public PauseCommand(ISong song)
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
            return Song.CanPause();
        }

        public void Execute(object parameter)
        {
            Song.Pause(new NAudio.Wave.WaveOut());
        }
    }
}
