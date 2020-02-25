using System.ComponentModel;

namespace MP3Player.Interfaces
{
    public interface IBaseViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string property);
    }
}