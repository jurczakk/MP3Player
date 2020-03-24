using NAudio.Wave;
using MP3Player.Interfaces.ViewModels;
using MP3Player.Interfaces.Models;
using MP3Player.Interfaces.Commands;

namespace MP3Player.ViewModels
{
    public class SongViewModel : BaseViewModel, ISongViewModel
    {
        private ISong song;
        public ISong Song 
        {
            get { return song; }
            set 
            {
                song = value; 
                OnPropertyChanged("Song"); 
            }
        }
        public WaveOut WaveOut { get; }
        public IPlayCommand Play { get; private set; }
        public IPauseCommand Pause { get; private set; }
        public IPlayNextCommand PlayNext { get; private set; }
        public IPlayBackCommand PlayBack { get; private set; }
        public SongViewModel() { }
        public SongViewModel(
            ISong song, 
            WaveOut waveOut, 
            IPlayCommand play, 
            IPauseCommand pause, 
            IPlayNextCommand playNext,
            IPlayBackCommand playBack)
        {
            Song = song;
            WaveOut = waveOut;
            Play = play;
            Pause = pause;
            PlayNext = playNext;
            PlayBack = playBack;
        }
    }
}