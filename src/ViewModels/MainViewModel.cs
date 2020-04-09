using MP3Player.Interfaces;

namespace MP3Player.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        public IPlaylistViewModel PlaylistViewModel { get; }
        public ISongViewModel SongViewModel { get; }
        public MainViewModel() { }
        public MainViewModel(ISongViewModel songViewModel, IPlaylistViewModel playlistViewModel)
        {
            SongViewModel = songViewModel;
            PlaylistViewModel = playlistViewModel;
        }
    }
}