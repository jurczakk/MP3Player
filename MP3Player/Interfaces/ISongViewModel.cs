using NAudio.Wave;
using System.Windows.Input;

namespace MP3Player.Interfaces
{
    public interface ISongViewModel
    {
        ICommand Pause { get; }
        ICommand PlayBack { get; }
        WaveOut WaveOut { get; }
        ICommand PlayNext { get; }
        ICommand Play { get; }
        ISong Song { get; set; }
    }
}