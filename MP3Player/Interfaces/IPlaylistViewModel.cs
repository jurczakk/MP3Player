namespace MP3Player.Interfaces
{
    public interface IPlaylistViewModel
    {
        IPlaylist Playlist { get; }
        IAddSongsCommand AddSongs { get; }
        IDeleteSongCommand DeleteSong { get; }
        IClearPlaylistCommand ClearPlaylist { get; }
    }
}