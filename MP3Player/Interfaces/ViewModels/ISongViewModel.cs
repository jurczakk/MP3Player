using MP3Player.Interfaces.Models;
using NAudio.Wave;
using System.Windows.Input;
using MP3Player.Interfaces.Commands;

namespace MP3Player.Interfaces.ViewModels
{
    public interface ISongViewModel
    {
        IPauseCommand Pause { get; }
        IPlayBackCommand PlayBack { get; }
        WaveOut WaveOut { get; }
        IPlayNextCommand PlayNext { get; }
        IPlayCommand Play { get; }
        ISong Song { get; set; }
    }
}