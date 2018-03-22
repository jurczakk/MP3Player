using NAudio.Wave;
namespace MP3Player.Models
{
    public class Song
    {
        private float volume;

        public string SongPath { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsPausing { get; set; }
        public AudioFileReader MP3 { get; set; }

        public float Volume
        {
            get => volume;
            set
            {
                volume = value;
                if (MP3 != null)
                    MP3.Volume = Volume;
            }
        }

        public Song(string path)
        {
            SongPath = path;
            if (!string.IsNullOrWhiteSpace(path))    
                MP3 = new AudioFileReader(path) { Volume = Volume };
            IsPlaying = false;
            IsPausing = false;
        }   

    }
}
