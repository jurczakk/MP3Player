using System.Windows;
using MP3Player.ViewModels;

namespace MP3Player.Views
{
    public partial class MP3Player : Window
    {
        public MP3Player()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
