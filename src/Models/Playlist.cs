using MP3Player.Interfaces.Models;
using System.Collections.Generic;

namespace MP3Player.Models
{
    public class Playlist : IPlaylist
    {
        public IList<string> SongsList { get; set; }
        public string SelectedSong { get; set; }
        public Playlist() { }
        public Playlist(IList<string> songsList)
        {
            SongsList = songsList;
        }
    }
}