using MP3Player.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MP3Player.Models
{
    public class Playlist : IPlaylist
    {
        public IList<string> SongsList { get; set; }
        public string SelectedSong { get; set; }

        public Playlist()
        {
            SongsList = new ObservableCollection<string> { };
        }
    }
}