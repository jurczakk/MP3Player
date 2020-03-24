using System.ComponentModel;

namespace MP3Player.Interfaces.ViewModels
{
    public interface IBaseViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string property);
    }
}