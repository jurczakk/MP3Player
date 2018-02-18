using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace MP3Player.Models
{
    public class Counter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void PropertyChangedMethod(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private Timer timer;
        public Timer Timer
        {
            get => timer;
            set => timer = value;
        }

        private Song song;
        public Song Song
        {
            get => song;
            set
            {
                song = value;
                positionValue = 0d;
            }
        }

        private double positionMax;
        public double PositionMax
        {
            get => positionMax; 
            set
            {
                positionMax = value;
                PropertyChangedMethod("PositionMax");
            }
        }

        private string timeText;
        public string TimeText
        {
            get => timeText; 
            set
            {
                timeText = value;
                PropertyChangedMethod("TimeText");
            }
        }
        private double positionValue;
        public double PositionValue
        {
            get => positionValue;
            set
            {
                positionValue = value;
                ChangePosition();
                PropertyChangedMethod("PositionValue");
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
