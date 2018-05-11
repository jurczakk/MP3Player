using System.Collections.ObjectModel;

namespace MP3Player.Models
{
    /// <summary>
    /// Playlist model
    /// </summary>
    public class Playlist 
    {
        public ObservableCollection<string> SongsList { get; set; }
        public string SelectedSong { get; set; }

        public Playlist() => 
            SongsList = new ObservableCollection<string> { };    
    }
}
