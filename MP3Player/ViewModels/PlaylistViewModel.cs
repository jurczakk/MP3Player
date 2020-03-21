using System.Windows.Input;
using MP3Player.Interfaces;

namespace MP3Player.ViewModels
{
    public class PlaylistViewModel : IPlaylistViewModel
    {
        public IPlaylist Playlist { get; }
        public ICommand AddSongs { get; private set; }
        public ICommand DeleteSong { get; private set; }
        public ICommand ClearPlaylist { get; private set; }
        public PlaylistViewModel() { }
        public PlaylistViewModel(IPlaylist playlist, ICommand addSongs, ICommand deleteSong, ICommand clearPlaylist)
        {
            Playlist = playlist;
            AddSongs = addSongs;
            DeleteSong = deleteSong;
            ClearPlaylist = clearPlaylist;
        }
    }
}