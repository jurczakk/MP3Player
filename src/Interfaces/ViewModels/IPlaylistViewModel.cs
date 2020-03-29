using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Models;

namespace MP3Player.Interfaces.ViewModels
{
    public interface IPlaylistViewModel
    {
        IPlaylist Playlist { get; }
        IAddSongsCommand AddSongs { get; }
        IDeleteSongCommand DeleteSong { get; }
        IClearPlaylistCommand ClearPlaylist { get; }
    }
}