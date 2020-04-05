using MP3Player.Interfaces.ViewModels;

namespace MP3Player.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        public IPlaylistViewModel PlaylistViewModel { get; }
        public ISongViewModel SongViewModel { get; }
        public MainViewModel()
        { 
        }
        public MainViewModel(IPlaylistViewModel playlistViewModel, ISongViewModel songViewModel)
        {
            PlaylistViewModel = playlistViewModel;
            SongViewModel = songViewModel;
        }
    }
}