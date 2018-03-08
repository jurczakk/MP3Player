using NAudio.Wave;

namespace MP3Player.Models
{
    public class Song
    {
        private string songName;
        private string songPath;
        private bool isPlaying;
        private bool isPausing;
        private AudioFileReader mp3;
        private float volume;

        public string SongName
        {
            get => songName;
            set => songName = value;
        }

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
                volume = float.Parse(value.ToString("0"));
                if (MP3 != null)
                    MP3.Volume = volume/100;
            }
        }

        public Song(string path)
        {
            SongPath = path;
            if (!string.IsNullOrWhiteSpace(path))
            {
                MP3 = new AudioFileReader(path) { Volume = Volume };
                SongName = path.Split('\\')[path.Split('\\').Length - 1];
            }
            IsPlaying = false;
            IsPausing = false;
            Volume = 0f;
        }

    }
}
