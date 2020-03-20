namespace MP3Player.Interfaces
{
    public interface IMainViewModel
    {
        IPlaylistViewModel PlaylistViewModel { get; }
        ISongViewModel SongViewModel { get; }
    }
}