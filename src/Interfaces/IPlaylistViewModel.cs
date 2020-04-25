using System.Windows.Input;

namespace MP3Player.Interfaces
{
    public interface IPlaylistViewModel
    {
        IPlaylist Playlist { get; }
        ICommand Clear { get; }
        ICommand DeleteSong { get; }
        ICommand GetSongs { get; }
    }
}