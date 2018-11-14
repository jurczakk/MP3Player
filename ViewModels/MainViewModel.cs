namespace MP3Player.ViewModels
{
    public class MainViewModel
    {
        private PlaylistViewModel playlistViewModel;
        private SongViewModel songViewModel;

        public PlaylistViewModel PlaylistViewModel { get { return playlistViewModel; } } 
        public SongViewModel SongViewModel { get { return songViewModel; } } 

        public MainViewModel() 
        {
            playlistViewModel = new PlaylistViewModel();
            songViewModel = new SongViewModel();
        }

    }
}