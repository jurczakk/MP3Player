using System.Windows.Input;

namespace MP3Player.Interfaces
{
    public interface IPlaylistViewModel
    {
        IPlaylist Playlist { get; }
        ICommand AddSongs { get; }
        ICommand DeleteSong { get; }
        ICommand ClearPlaylist { get; }
    }
}