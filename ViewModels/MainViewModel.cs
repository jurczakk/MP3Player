namespace MP3Player.ViewModels
{
    public class MainViewModel
    {
        private PlaylistViewModel playlistViewModel;
        public PlaylistViewModel PlaylistViewModel => playlistViewModel;
        private SongViewModel songViewModel;
        public SongViewModel SongViewModel => songViewModel;
        public MainViewModel() 
        {
            playlistViewModel = new PlaylistViewModel();
            songViewModel = new SongViewModel();
        }
    }
}
