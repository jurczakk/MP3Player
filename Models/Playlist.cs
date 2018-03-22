using System.Collections.ObjectModel;
using System.ComponentModel;
namespace MP3Player.Models
{
    public class Playlist : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private string selectedSong;

        public string SelectedSong
        {
            get { return selectedSong; }
            set { selectedSong = value; OnPropertyChanged("SelectedSong"); }       
        }

        public ObservableCollection<string> SongsList { get; set; }

        public Playlist() => SongsList = new ObservableCollection<string> { };
            
    }
}
