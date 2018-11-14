using System.Collections.ObjectModel;

namespace MP3Player.Models
{
    public class Playlist 
    {
        public ObservableCollection<string> SongsList { get; set; }
        public string SelectedSong { get; set; }
        public Playlist()
        {
            SongsList = new ObservableCollection<string> { };
        }
    }
}