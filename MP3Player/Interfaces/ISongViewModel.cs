using MP3Player.Enums;
using NAudio.Wave;
using System.Windows.Input;

namespace MP3Player.Interfaces
{
    public interface ISongViewModel
    {
        ICommand PauseSong { get; }
        ICommand PlayBackSong { get; }
        WaveOut Player { get; }
        ICommand PlayNextSong { get; }
        ICommand PlaySong { get; }
        ISong Song { get; set; }
        bool CanPauseSong();
        bool CanPlayMusic(IPlaylist playlist);
        void PlayMusic(IPlaylist playlist);
        void SongPause();
        void UniversalPlay(IPlaylist playlist, PlayType playType);
    }
}