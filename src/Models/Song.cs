using MP3Player.Abstracts;
using MP3Player.Interfaces;
using NAudio.Wave;
using System;

namespace MP3Player.Models
{
    public class Song : NotifyPropertyChanged, ISong
    {
        private double positionMax;
        private string timeText;
        private double positionValue;
        private float volume;

        public double PositionMax
        {
            get
            {
                return positionMax;
            }
            set
            {
                positionMax = value;
                OnPropertyChanged("PositionMax");
            }
        }
        public string TimeText
        {
            get
            {
                return timeText;
            }
            set
            {
                timeText = value;
                OnPropertyChanged("TimeText");
            }
        }
        public double PositionValue
        {
            get
            {
                return positionValue;
            }
            set
            {
                positionValue = value;
                MP3.CurrentTime = TimeSpan.FromSeconds(positionValue);
                OnPropertyChanged("PositionValue");
            }
        }
        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = float.Parse(value.ToString("0"));
                if (MP3 != null)
                    MP3.Volume = volume / 100;
                OnPropertyChanged("Volume");
            }
        }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsPausing { get; set; }
        public AudioFileReader MP3 { get; set; }
        public Song() { }
        public Song(string path, float volume = 0f)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                MP3 = new AudioFileReader(path) { Volume = volume };
                Name = System.IO.Path.GetFileName(path);
                PositionMax = MP3.TotalTime.TotalSeconds;
            }
            Path = path;
            Volume = volume;
            IsPlaying = IsPausing = false;
        }
        public void ChangePosition()
        {
            MP3.CurrentTime = TimeSpan.FromSeconds(PositionValue);
            IsPlaying = false;
            IsPausing = false;
        }
    }
}