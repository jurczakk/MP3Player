namespace MP3Player.ViewModels
{
    public class MainViewModel
    {
        private PlaylistViewModel playlistViewModel;
        private SongViewModel songViewModel;
        public PlaylistViewModel PlaylistViewModel => playlistViewModel;
        public SongViewModel SongViewModel => songViewModel;
        public MainViewModel() 
        {
            playlistViewModel = new PlaylistViewModel();
            songViewModel = new SongViewModel();
        }
    }
}
