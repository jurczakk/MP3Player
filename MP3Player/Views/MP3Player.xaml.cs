using System.Windows;
using MP3Player.Models;

namespace MP3Player.Views
{
    public partial class MP3Player : Window
    {
        public MP3Player()
        {
            InitializeComponent();

            DataContext = Config.GetMainViewModel();
        }
    }
}
