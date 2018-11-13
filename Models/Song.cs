using NAudio.Wave;

namespace MP3Player.Models
{
    public class Song
    {
        private float volume;

        public string SongName { get; set; }
        public string SongPath { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsPausing { get; set; }
        public AudioFileReader MP3 { get; set; }
        
        public float Volume
        {
            get { return volume; }
            set
            {
                volume = float.Parse(value.ToString("0"));
                if (MP3 == null)
                    return;
                MP3.Volume = volume / 100;
            }
        }

        /// <summary>
        /// if Path is not null, 
        /// then we can create a new instance of AudiofileReader
        /// </summary>
        public Song(string _path)
        {
            SongPath = _path;
            if (!string.IsNullOrWhiteSpace(_path)) 
            {
                MP3 = new AudioFileReader(_path) { Volume = Volume };
                SongName = System.IO.Path.GetFileName(_path);
            }
            IsPlaying = false;
            IsPausing = false;
            Volume = 0f;
        }
    }
}