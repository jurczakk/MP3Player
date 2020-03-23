using MP3Player.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static MP3Player.Extensions.SongExtension;

namespace MP3Player.Commands
{        //    var play = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.PlayMusic(x as Playlist, waveOut));
         //    var pause = new MainCommand(x => song.CanPauseSong(), x => song.SongPause(waveOut));
         //    var playnext = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.UniversalPlay(x as IPlaylist, PlayType.Next, waveOut));
         //    var playback = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.UniversalPlay(x as IPlaylist, PlayType.Back, waveOut));
    public class PauseCommand : ICommand
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
            Song.SongPause();
        }
    }
}
