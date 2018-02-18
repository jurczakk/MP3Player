using System.Collections.ObjectModel;
using System.ComponentModel;
namespace MP3Player.Models
{
    public class Playlist : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private ObservableCollection<string> songsList;
        public ObservableCollection<string> SongsList
        {
            get => songsList;
            set => songsList = value;
        }

        private string selectedSong;
        public string SelectedSong
        {
            get => selectedSong;
            set
            {
                selectedSong = value;
                OnPropertyChanged("SelectedSong");
            }       
        }
        public Playlist() => songsList = new ObservableCollection<string> { };
            
    }
}
