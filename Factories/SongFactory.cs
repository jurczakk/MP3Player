using MP3Player.Models;
using NAudio.Wave;

namespace MP3Player.Factories
{
    public static class SongFactory
    {
        public static Song GetSong(string path, float volume)
        {
            try { var audioFileReader = new AudioFileReader(path) { Volume = volume }; }
            catch { return null; }

            return new Song(path);
        }
    }
}
