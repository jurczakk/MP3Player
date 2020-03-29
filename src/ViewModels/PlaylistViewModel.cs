using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Models;
using MP3Player.Interfaces.ViewModels;

namespace MP3Player.ViewModels
{
    public class PlaylistViewModel : IPlaylistViewModel
    {
        public IPlaylist Playlist { get; }
        public IAddSongsCommand AddSongs { get; private set; }
        public IDeleteSongCommand DeleteSong { get; private set; }
        public IClearPlaylistCommand ClearPlaylist { get; private set; }
        public PlaylistViewModel() { }
        public PlaylistViewModel(
            IPlaylist playlist, 
            IAddSongsCommand addSongs, 
            IDeleteSongCommand deleteSong, 
            IClearPlaylistCommand clearPlaylist)
        {
            Playlist = playlist;
            AddSongs = addSongs;
            DeleteSong = deleteSong;
            ClearPlaylist = clearPlaylist;
        }
    }
}