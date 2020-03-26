using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Helpers;
using NAudio.Wave;
using System;
using System.Windows.Input;

namespace MP3Player.Commands
{
    public class PauseCommand : ICommand, IPauseCommand
    {
        private readonly ISongHelpers SongHelpers;
        private readonly IWavePlayer WavePlayer;

        public PauseCommand(ISongHelpers songHelpers, IWavePlayer wavePlayer)
        {
            SongHelpers = songHelpers;
            WavePlayer = wavePlayer;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return SongHelpers.CanPause();
        }

        public void Execute(object parameter)
        {
            SongHelpers.Pause(WavePlayer);
        }
    }
}
