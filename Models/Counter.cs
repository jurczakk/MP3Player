using MP3Player.ViewModels;
using System;
using System.Windows.Forms;

namespace MP3Player.Models
{
    /// <summary>
    /// Counter class
    /// </summary>
    public class Counter : BaseViewModel
    {
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
            set { positionMax = value; OnPropertyChanged("PositionMax"); }
        }

        public string TimeText
        {
            get { return timeText; } 
            set { timeText = value; OnPropertyChanged("TimeText"); }
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

        public Counter()
        {
            Timer = new Timer();
            Song = new Song(null);
        }

        public void ChangePosition() => 
            song.MP3.CurrentTime = TimeSpan.FromSeconds(PositionValue);


        /// <summary>
        /// We count currently song time, we will use that method in the SongViewModel
        /// </summary>
        /// <param name="e"></param>
        public void CountTime(EventHandler e)
        {
            Timer.Tick += new EventHandler(e);
            Timer.Interval = 1000;
            Timer.Start();
        }
    }
}
