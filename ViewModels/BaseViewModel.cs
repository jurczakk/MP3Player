using System.ComponentModel;

namespace MP3Player.ViewModels
{
    /// <summary>
    /// BaseViewModel, which we will implement in every other ViewModel, 
    /// i do that because i dont want to repeat all the time INotifyPropertyChanged
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
