using MP3Player.Enums;
using MP3Player.Interfaces.Models;
using NAudio.Wave;

namespace MP3Player.Interfaces.Helpers
{
    public interface ISongHelpers
    {
        bool CanPause();
        bool CanPlay(IPlaylist playlist);
        void Pause(IWavePlayer wavePlayer);
        void Play(IPlaylist playlist, IWavePlayer wavePlayer);
        void UniversalPlay(IPlaylist playlist, PlayType playType, IWavePlayer wavePlayer);
    }
}