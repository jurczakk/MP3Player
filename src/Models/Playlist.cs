using System.Collections.Generic;
using System;
using MP3Player.Interfaces;

namespace MP3Player.Models
{
    public class Playlist : IPlaylist
    {
        public Tuple<int, string> SelectedSong { get; set; }
        public IList<Tuple<int, string>> SongsList { get; set; }
        public Playlist() { }
        public Playlist(IList<Tuple<int, string>> songsList)
        {
            SongsList = songsList;
        }
    }
}