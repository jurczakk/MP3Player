using MP3Player.Interfaces.Models;
using MP3Player.ViewModels;
using NAudio.Wave;
using System;
using System.Timers;

namespace MP3Player.Models
{
    public class Song : BaseViewModel, ISong
    {
        private double positionMax;
        private string timeText;
        private double positionValue;
        private float volume;

        public double PositionMax
        {
            get { return positionMax; }
            set 
            { 
                positionMax = value; 
                OnPropertyChanged("PositionMax"); 
            }
        }
        public string TimeText
        {
            get { return timeText; }
            set 
            { 
                timeText = value; 
                OnPropertyChanged("TimeText"); 
            }
        }
        public double PositionValue
        {
            get { return positionValue; }
            set
            {
                positionValue = value;
                ChangePosition();
                OnPropertyChanged("PositionValue");
            }
        }
        public float Volume
        {
            get { return volume; }
            set
            {
                volume = float.Parse(value.ToString("0"));
                if (MP3 != null) MP3.Volume = volume / 100;
                OnPropertyChanged("Volume");
            }
        }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsPausing { get; set; }
        public AudioFileReader MP3 { get; set; }

        public void ChangePosition()
        {
            MP3.CurrentTime = TimeSpan.FromSeconds(PositionValue);
        }

        public Song() { }
        public Song(string path = "", float volume = 0f)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                MP3 = new AudioFileReader(path) { Volume = volume };
                Name = System.IO.Path.GetFileName(path);
            }
            Path = path;
            Volume = volume;
            IsPlaying = IsPausing = false;
        }

        public Song( 
            string name, 
            string path, 
            bool isPlaying, 
            bool isPausing, 
            AudioFileReader mp3)
        {
            Name = name;
            Path = path;
            IsPlaying = isPlaying;
            IsPausing = isPausing;
            MP3 = mp3;
        }
    }
}