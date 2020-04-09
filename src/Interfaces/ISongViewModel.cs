using System.Windows.Input;

namespace MP3Player.Interfaces
{
    public interface ISongViewModel
    {
        ISong Song { get; set; }
        ICommand Pause { get; }
        ICommand PlayBack { get; }
        ICommand PlayNext { get; }
        ICommand Play { get; }
    }
}