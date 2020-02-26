using MP3Player.Abstracts;

namespace MP3Player.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        public IPlaylistViewModel PlaylistViewModel { get; }
        public ISongViewModel SongViewModel { get; }
        public MainViewModel()
        {
            PlaylistViewModel = new PlaylistViewModel();
            SongViewModel = new SongViewModel();
        }
    }
}