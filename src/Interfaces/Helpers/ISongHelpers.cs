using MP3Player.Enums;
using MP3Player.Interfaces.Models;

namespace MP3Player.Interfaces.Helpers
{
    public interface ISongHelpers
    {
        bool CanPause();
        bool CanPlay(IPlaylist playlist);
        void Pause();
        void Play(IPlaylist playlist);
        void UniversalPlay(IPlaylist playlist, PlayType playType);
    }
}