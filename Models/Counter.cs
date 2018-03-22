using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace MP3Player.Models
{
    public class Counter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void PropertyChangedMethod(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private Song song;
        private double positionMax;
        private string timeText;
        private double positionValue;

        public Timer Timer { get; set; }
        public Song Song
        {
            get { return song; }
            set { song = value; positionValue = 0d; }
        }
        public double PositionMax
        {
            get { return positionMax; }
            set { positionMax = value; PropertyChangedMethod("PositionMax"); }
        }
        public string TimeText
        {
            get { return timeText; }
            set { timeText = value; PropertyChangedMethod("TimeText"); }
        }
       
        public double PositionValue
        {
            get { return positionValue; }
            set
            {
                positionValue = value;
                ChangePosition();
                PropertyChangedMethod("PositionValue");
            }
        }

        public Counter()
        {
            Timer = new Timer();
            Song = new Song(null);
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
