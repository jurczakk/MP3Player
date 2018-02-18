using NAudio.Wave;
namespace MP3Player.Models
{
    public class Song
    {
        private string songPath;
        private bool isPlaying;
        private bool isPausing;
        private AudioFileReader mp3;
        private float volume;

        public string SongPath
        {
            get => songPath;
            set => songPath = value;
        }

        public bool IsPlaying
        {
            get => isPlaying;
            set => isPlaying = value;
        }

        public bool IsPausing
        {
            get => isPausing;
            set => isPausing = value;
        }

        public AudioFileReader MP3
        {
            get => mp3;
            set => mp3 = value;
        }

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
