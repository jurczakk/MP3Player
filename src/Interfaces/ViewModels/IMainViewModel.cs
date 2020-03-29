namespace MP3Player.Interfaces.ViewModels
{
    public interface IMainViewModel
    {
        IPlaylistViewModel PlaylistViewModel { get; }
        ISongViewModel SongViewModel { get; }
    }
}