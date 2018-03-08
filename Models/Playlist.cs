using System.Collections.ObjectModel;

namespace MP3Player.Models
{
    public class Playlist 
    {
        private ObservableCollection<string> songsList;
        private string selectedSong;

        public ObservableCollection<string> SongsList
        {
            get => songsList;
            set => songsList = value;
        }

        public string SelectedSong
        {
            get => selectedSong;
            set => selectedSong = value;
        }

        public Playlist() => songsList = new ObservableCollection<string> { };    
    }
}
