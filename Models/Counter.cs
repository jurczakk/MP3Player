using MP3Player.ViewModels;
using System;
using System.Windows.Forms;

namespace MP3Player.Models
{
    public class Counter : BaseViewModel
    {
        private Timer timer;
        private Song song;
        private double positionMax;
        private string timeText;
        private double positionValue;

        public Timer Timer
        {
            get => timer;
            set => timer = value;
        }

        public Song Song
        {
            get => song;
            set
            {
                song = value;
                positionValue = 0d;
            }
        }

        public double PositionMax
        {
            get => positionMax; 
            set
            {
                positionMax = value;
                OnPropertyChanged("PositionMax");
            }
        }

        public string TimeText
        {
            get => timeText; 
            set
            {
                timeText = value;
                OnPropertyChanged("TimeText");
            }
        }
        public double PositionValue
        {
            get => positionValue;
            set
            {
                positionValue = value;
                ChangePosition();
                OnPropertyChanged("PositionValue");
            }
        }

        public Counter()
        {
            timer = new Timer();
            song = new Song(null);
        }

        public void ChangePosition() => song.MP3.CurrentTime = TimeSpan.FromSeconds(PositionValue);
        public void CountTime(EventHandler e)
        {
            Timer.Tick += new EventHandler(e);
            Timer.Interval = 1000;
            Timer.Start();
        }
    }
}
