using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace MP3Player.Models
{
    public class Counter : INotifyPropertyChanged
    {
        private Timer timer;
        private Song song;
        private double positionMax;
        private string timeText;
        private double positionValue;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void PropertyChangedMethod(string property) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            
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
                PropertyChangedMethod("PositionMax");
            }
        }

        public string TimeText
        {
            get => timeText; 
            set
            {
                timeText = value;
                PropertyChangedMethod("TimeText");
            }
        }
        
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
