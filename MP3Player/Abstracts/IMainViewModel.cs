namespace MP3Player.Abstracts
{
    public interface IMainViewModel
    {
        IPlaylistViewModel PlaylistViewModel { get; }
        ISongViewModel SongViewModel { get; }
    }
}