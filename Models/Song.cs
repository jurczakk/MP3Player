using NAudio.Wave;

namespace MP3Player.Models
{
    public class Song
    {
        private float volume;
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsPausing { get; set; }
        public AudioFileReader MP3 { get; set; }

        public float Volume
        {
            get { return volume; }
            set
            {
                volume = float.Parse(value.ToString("0"));
                if (MP3 != null)
                    MP3.Volume = volume / 100;
            }
        }

        public Song(string path)
        {
            Path = path;
            if (!string.IsNullOrWhiteSpace(path))
            {
                MP3 = new AudioFileReader(path) { Volume = Volume };
                Name = System.IO.Path.GetFileName(path);
            }
            IsPlaying = false;
            IsPausing = false;
            Volume = 0f;
        }
    }
}