using NAudio.Wave;
using System.Windows.Input;
using MP3Player.Interfaces;

namespace MP3Player.ViewModels
{
    public class SongViewModel : BaseViewModel, ISongViewModel
    {
        public ISong Song 
        {
            get { return Song; }
            set 
            { 
                Song = value; 
                OnPropertyChanged("Song"); 
            }
        }
        public WaveOut WaveOut { get; }
        public ICommand Play { get; private set; }
        public ICommand Pause { get; private set; }
        public ICommand PlayNext { get; private set; }
        public ICommand PlayBack { get; private set; }
        public SongViewModel() { }
        public SongViewModel(ISong song, WaveOut waveOut, ICommand play, ICommand pause, ICommand playNext, ICommand playBack)
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