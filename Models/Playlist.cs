using System.Collections.ObjectModel;
using System.ComponentModel;
namespace MP3Player.Models
{
    public class Playlist : INotifyPropertyChanged
    {
        private ObservableCollection<string> songsList;
        private string selectedSong;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            
        public ObservableCollection<string> SongsList
        {
            get => songsList;
            set => songsList = value;
        }

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
