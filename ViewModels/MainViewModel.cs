namespace MP3Player.ViewModels
{
    public class MainViewModel
    {
        public PlaylistViewModel PlaylistViewModel { get; }
        public SongViewModel SongViewModel { get; }
        public MainViewModel() 
        {
            PlaylistViewModel = new PlaylistViewModel();
            SongViewModel = new SongViewModel();
        }
    }
}