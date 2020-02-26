using System.ComponentModel;

namespace MP3Player.Abstracts
{
    public interface IBaseViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string property);
    }
}