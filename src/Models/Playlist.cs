using System.Collections.Generic;
using MP3Player.Interfaces;

namespace MP3Player.Models
{
    public class Playlist : IPlaylist
    {
        public ISongData SelectedSong { get; set; }
        public IList<ISongData> SongsList { get; set; }
        public Playlist() { }
        public Playlist(IList<ISongData> songsList)
        {
            SongsList = songsList;
        }
    }
}