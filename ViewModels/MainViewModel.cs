namespace MP3Player.ViewModels
{
    /// <summary>
    /// MainViewModel
    /// I use that to pass our 2 ViewModels to our View 
    /// </summary>
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
