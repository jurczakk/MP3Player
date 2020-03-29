using MP3Player.Interfaces.ViewModels;
using System.ComponentModel;

namespace MP3Player.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IBaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}