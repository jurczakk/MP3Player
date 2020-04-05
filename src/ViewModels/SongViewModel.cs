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
            get 
            { 
                return song; 
            }
            set 
            {
                song = value; 
                OnPropertyChanged("Song"); 
            }
        }
        public IPlayCommand Play { get; private set; }
        public IPauseCommand Pause { get; private set; }
        public IPlayNextCommand PlayNext { get; private set; }
        public IPlayBackCommand PlayBack { get; private set; }
        public SongViewModel()
        { 
        }
        public SongViewModel(
            ISong song,
            IPlayCommand play, 
            IPauseCommand pause, 
            IPlayNextCommand playNext,
            IPlayBackCommand playBack)
        {
            Song = song;
            Play = play;
            Pause = pause;
            PlayNext = playNext;
            PlayBack = playBack;
        }
    }
}